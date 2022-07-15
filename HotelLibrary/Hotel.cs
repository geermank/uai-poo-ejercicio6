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
        private ISet<Persona> personas;
        private List<Estadia> estadias;
        private List<Cancelacion> cancelaciones;

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
            this.estadias = new List<Estadia>();
            this.personas = new HashSet<Persona>();
            this.cancelaciones = new List<Cancelacion>();
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

        public float ObtenerMontoReservaMinimo(Habitacion habitacion, 
                                               List<Adicional> adicionales, 
                                               DateTime checkin,
                                               DateTime checkout)
        {
            int cantidadDeNoches = (checkout - checkin).Days;
            return new SimuladorReserva().SimularDepositoMinimo(habitacion, adicionales, cantidadDeNoches);
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

            RegistrarDatosPersonas(personas);
            reservas.Add(posibleReserva);

            ExitoEnReserva(posibleReserva);
        }

        public void ConfirmarEstadia(Reserva reserva)
        {
            Estadia estadia = reserva.ConfirmarRecepcion();
            estadias.Add(estadia);
        }

        public float ObtenerCostoDeCancelacion(Reserva reserva)
        {
            return reserva.SimularCostoCancelacion(DateTime.Now);
        }

        public Cancelacion CancelarReserva(Reserva reserva)
        {
            var estadiaReserva = (from e in estadias
                                  where e.Reserva.Equals(reserva)
                                  select e);
            if (estadiaReserva != null || reserva.Cancelada)
            {
                // no se puede cancelar una reserva confirmada o cancelada
                return null;
            }

            Cancelacion cancelacion = reserva.Cancelar(DateTime.Now);
            cancelaciones.Add(cancelacion);
            return cancelacion;
        }

        public void AgregarAdicional(Estadia estadia, Adicional adicional)
        {
            estadia.RegistrarAdicional(adicional);
        }

        public void RealizarCheckout(Estadia estadia)
        {
            estadia.Concluir();
        }

        public Habitacion ObtenerHabitacionMasReservada(DateTime rangoInf, DateTime rangoSup)
        {
            Habitacion habitacionMasReservada = null;
            int numeroDeVecesReservada = 0;

            foreach(Habitacion h in habitacionesDisponibles)
            {
                int numReservas = (from r in reservas
                                   where r.Checkin >= rangoInf 
                                   && r.Checkout <= rangoSup
                                   select r).Count();
                if (numReservas >= numeroDeVecesReservada)
                {
                    habitacionMasReservada = h;
                    numeroDeVecesReservada = numReservas;
                }
            }

            return habitacionMasReservada;
        }

        public Persona ObtenerPasajeroQueMasSeHospedo()
        {
            Persona persona = null;
            int cantidadDeVecesHospedado = 0;

            foreach(Persona p in personas)
            {
                var numeroDeVecesHospedado = (from r in reservas
                                              where r.Personas.Contains(p)
                                              select r).Count();
                if (numeroDeVecesHospedado >= cantidadDeVecesHospedado)
                {
                    persona = p;
                    cantidadDeVecesHospedado = numeroDeVecesHospedado;
                }
            }

            return persona;
        }

        public float RecaudacionTotalDelHotel(DateTime rangoInf, DateTime rangoSup)
        {
            float recaudacionTotal = 0;

            // recaudacion cancelaciones
            recaudacionTotal += (from c in cancelaciones
                                 where c.FechaCancelacion >= rangoInf
                                 && c.FechaCancelacion <= rangoSup
                                 select c).Sum(c => c.CostoDeCancelacion);

            // recaudacion estadias finalizadas
            recaudacionTotal += estadias.FindAll(e => e.Concluida && e.Reserva.Checkout >= rangoInf && e.Reserva.Checkout <= rangoSup)
                                        .Sum(e => e.ImporteTotalFacturado);

            // recaudacion estadias en curso
            recaudacionTotal += estadias.FindAll(e => !e.Concluida && e.Reserva.Checkin >= rangoInf && e.Reserva.Checkout <= rangoSup)
                                         .Sum(e => e.Reserva.Deposito);

            // recaudacion reservas no canceladas ni confirmadas
            List<Reserva> reservasConfirmadas = estadias.Select(e => e.Reserva).ToList();
            var reservasNoCanceladasNiConfirmadas = (from r in reservas
                                                     where !reservasConfirmadas.Contains(r)
                                                     && !r.Cancelada
                                                     select r);
            recaudacionTotal += reservasNoCanceladasNiConfirmadas.Sum(r => r.Deposito);

            return recaudacionTotal;
        }

        public Habitacion ObtenerHabitacionMasOcupada(DateTime rangoInf, DateTime rangoSup)
        {
            var habitacionesReservadas = (from e in estadias
                     where e.Reserva.Checkin >= rangoInf 
                     && e.Reserva.Checkout <= rangoSup
                     select e.Reserva.Habitacion);

            Habitacion habitacionMasReservada = null;
            int cantVecesReservada = 0;

            foreach (var habitacion in habitacionesReservadas)
            {
                int cant = habitacionesReservadas.Count(h => h.Equals(habitacion));
                if (cant >= cantVecesReservada)
                {
                    habitacionMasReservada = habitacion;
                    cantVecesReservada = cant;
                }
            }

            return habitacionMasReservada;
        }

        private bool VerificarHabitacionReservada(Habitacion habitacion, DateTime checkin, DateTime checkout)
        {
            var result = (from reserva in reservas
                          where reserva.Checkin >= checkin && reserva.Checkout <= checkout
                          || checkin >= reserva.Checkin && checkout <= reserva.Checkout
                          select reserva).ToList();
            return result.Count > 0;
        }

        private void RegistrarDatosPersonas(List<Persona> personas)
        {
            foreach (Persona persona in personas)
            {
                this.personas.Add(persona);
            }
        }
    }
}