using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelLibrary
{
    public class CreadorDeArtefactos
    {
        public List<Artefacto> CrearArtefactosHabitaciones()
        {
            return new List<Artefacto> { 
                new Artefacto("Television"),
                new Artefacto("Aire"),
                new Artefacto("Frigobar")
            };
        }
    }
}