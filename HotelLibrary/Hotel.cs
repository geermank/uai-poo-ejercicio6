using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HotelLibrary.Delegate;

namespace HotelLibrary
{
    public class Hotel
    {
        public event delErrorEnReserva ErrorEnReserva;
        public event delExitoEnReserva ExitoEnReserva;

        private List<Habitacion> habitacionesDisponibles;

        private List<string> adicionalesDisponibles;
        private List<Artefacto> artefactosDisponibles;
        private List<Caracteristica> caracteristicasDisponibles;

        private CreadorDeHabitaciones creadorDeHabitaciones;
        private CreadorDeAdicionales creadorDeAdicionales;

        private List<Reserva> reservas;
        private List<Estadia> estadias;

        public Hotel(CreadorDeArtefactos creadorDeArtefactos, 
                     CreadorDeAdicionales creadorDeAdicionales,
                     CreadorDeCaracteristicas creadorDeCaracteristicas,
                     CreadorDeHabitaciones creadorDeHabitaciones)
        {
            this.creadorDeHabitaciones = creadorDeHabitaciones;
            this.creadorDeAdicionales = creadorDeAdicionales;
            this.adicionalesDisponibles = creadorDeAdicionales.AdicionalesDisponibles();
            this.artefactosDisponibles = creadorDeArtefactos.CrearArtefactosHabitaciones();
            this.caracteristicasDisponibles = creadorDeCaracteristicas.CrearCaracteristicasDeHabitaciones();
        }

        public List<Artefacto> Artefactos
        {
            get { return artefactosDisponibles; }
        }

        public List<Caracteristica> Caracteristicas
        {
            get { return caracteristicasDisponibles; }
        }

        public List<string> Adicionales
        {
            get { return adicionalesDisponibles; }
        }

        public List<Reserva> Reservas
        {
            get { return reservas; }
        }

        public void CrearHabitacionSimple(int numero, 
                                          List<Artefacto> artefactos, 
                                          List<Caracteristica> caracteristicas)
        {
            habitacionesDisponibles.Add(creadorDeHabitaciones.CrearHabitacionSimple(numero, artefactos, caracteristicas));
        }

        public void CrearHabitacionDoble(int numero,
                                          List<Artefacto> artefactos,
                                          List<Caracteristica> caracteristicas)
        {
            habitacionesDisponibles.Add(creadorDeHabitaciones.CrearHabitacionDobleMatrimonial(numero, artefactos, caracteristicas));
        }

        public void CrearHabitacionTriple(int numero,
                                  List<Artefacto> artefactos,
                                  List<Caracteristica> caracteristicas)
        {
            habitacionesDisponibles.Add(creadorDeHabitaciones.CrearHabitacionTriple(numero, artefactos, caracteristicas));
        }

        public void CrearHabitacionCuadruple(int numero,
                          List<Artefacto> artefactos,
                          List<Caracteristica> caracteristicas)
        {
            habitacionesDisponibles.Add(creadorDeHabitaciones.CrearHabitacionCuadruple(numero, artefactos, caracteristicas));
        }

        public Persona CrearPersona(int dni, string nombre, string apellido, DateTime fechaNacimiento)
        {
            return new Persona(dni, nombre, apellido, fechaNacimiento);
        }

        public Adicional CrearAdicionalCuna()
        {
            return creadorDeAdicionales.CrearAdicionalCuna();
        }

        public Adicional CrearAdicionalFrigobar(float importe)
        {
            return creadorDeAdicionales.CrearConsumoFrigobar(importe);
        }

        public void ReservarHabitacion(Habitacion habitacion, 
                                       List<Persona> personas,
                                       DateTime checkin,
                                       DateTime checkout,
                                       List<Adicional> adicionales,
                                       float deposito)
        {
            if (personas.Count == 0)
            {
                ErrorEnReserva("Se debe indicar los datos de las personas");
                return;
            }

            if (VerificarHabitacionReservada(habitacion, checkin, checkout))
            {
                ErrorEnReserva("La habitacion está reservada en la fecha indicada");
                return;
            }
            
            Reserva posibleReserva = new Reserva(habitacion, checkin, checkout, adicionales, personas, deposito);
            
            float depositoMinimo = posibleReserva.ObtenerImporteMinimoDeLaReserva();
            if (depositoMinimo > deposito)
            {
                ErrorEnReserva("El deposito tiene que ser de al menos " + depositoMinimo);
                return;
            }

            reservas.Add(posibleReserva);

            ExitoEnReserva(posibleReserva);
        }

        private bool VerificarHabitacionReservada(Habitacion habitacion, DateTime checkin, DateTime checkout)
        {
            var result = (from reserva in reservas
                          where reserva.Checkin >= checkin && reserva.Checkout <= checkout
                          || checkin >= reserva.Checkin && checkout <= reserva.Checkout
                          select reserva).ToList();
            return result.Count > 0;
        }
    }
}