using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelLibrary
{
    public class Reserva
    {
        private string codigoReserva;
        private Habitacion habitacion;
        private DateTime checkin;
        private DateTime checkout;
        private List<Adicional> adicionales;
        private List<Persona> personas;
        private float deposito;
        private bool cancelada;

        public Reserva(Habitacion habitacion, 
                       DateTime checkin, 
                       DateTime checkout, 
                       List<Adicional> adicionales,
                       List<Persona> personas,
                       float deposito)
        {
            this.codigoReserva = Guid.NewGuid().ToString();
            this.habitacion = habitacion;
            this.checkin = checkin;
            this.checkout = checkout;
            this.adicionales = adicionales;
            this.personas = personas;
            this.deposito = deposito;
        }

        public string CodigoReserva
        {
            get { return codigoReserva; }
        }

        public Habitacion Habitacion
        {
            get { return habitacion; }
        }

        public float Deposito
        {
            get {
                return deposito;
            }
        }

        public float ImporteAFacturar
        {
            get {
                float costoAdicionales = adicionales.Sum(a => a.Precio(CantidadDeNoches));
                return habitacion.Precio(CantidadDeNoches) + costoAdicionales;
            }
        }

        public int CantidadDeNoches
        {
            get { return (checkout - checkin).Days; }
        }

        public float ImportePendienteDePago
        {
            get
            {
                return ImporteAFacturar - deposito;
            }
        }

        public DateTime Checkin
        {
            get { return checkin; }
        }

        public DateTime Checkout
        {
            get { return checkout; }
        }

        public List<Adicional> Adicionales
        {
            get { return adicionales; }
        }

        public List<Persona> Personas
        {
            get { return personas; }
        }

        public bool Cancelada
        {
            get { return cancelada; }
        }

        public Cancelacion Cancelar(DateTime fechaDeCancelacion)
        {
            if (cancelada)
            {
                return null;
            }
            cancelada = true;
            return new Cancelacion(this, fechaDeCancelacion);
        }

        public float SimularCostoCancelacion(DateTime fechaDeCancelacion)
        {
            float importe = Cancelar(fechaDeCancelacion).CostoDeCancelacion;
            cancelada = false;
            return importe;
        }

        public float ObtenerImporteMinimoDeLaReserva()
        {
            return (ImporteAFacturar * 10) / 100;
        }

        public Estadia ConfirmarRecepcion()
        {
            return new Estadia(this);
        }
    }
}