using System;
using System.Collections.Generic;

namespace HotelLibrary
{
    public class Cancelacion
    {
        private Reserva reserva;
        private DateTime fechaDeCancelacion;

        public Cancelacion(Reserva reserva,
                                DateTime fechaDeCancelacion)
        {
            this.reserva = reserva;
            this.fechaDeCancelacion = fechaDeCancelacion;
        }

        public float CostoDeCancelacion
        {
            get
            {
                float importeFacturadoPorElHotel;
                if ((reserva.Checkin - fechaDeCancelacion).Days > 7)
                {
                    // el cliente no pierde nada
                    importeFacturadoPorElHotel = 0f;
                }
                else if ((reserva.Checkin - fechaDeCancelacion).Days < 2)
                {
                    // el cliente pierde el minimo de la reserva
                    importeFacturadoPorElHotel = reserva.ObtenerImporteMinimoDeLaReserva();
                }
                else
                {
                    // el cliente pierde el 50% del minimo
                    importeFacturadoPorElHotel = reserva.ObtenerImporteMinimoDeLaReserva() / 2;
                }
                return importeFacturadoPorElHotel;
            }
        }
    }
}