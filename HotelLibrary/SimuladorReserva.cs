using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelLibrary
{
    public class SimuladorReserva
    {
        public float SimularCostoReserva(Habitacion habitacion, List<Adicional> adicionales, int cantidadDeNoches)
        {
            float costoAdicionales = adicionales.Sum(a => a.Precio(cantidadDeNoches));
            return habitacion.Precio(cantidadDeNoches) + costoAdicionales;
        }

        public float SimularDepositoMinimo(Habitacion habitacion, List<Adicional> adicionales, int cantidadDeNoches)
        {
            return (SimularCostoReserva(habitacion, adicionales, cantidadDeNoches) * 10) / 100;
        }
    }
}