using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelLibrary
{
    public class CreadorDeCaracteristicas
    {
        public List<Caracteristica> CrearCaracteristicasDeHabitaciones()
        {
            return new List<Caracteristica> { 
                new VistaAlMar() 
            };
        }
    }
}