using System.Collections.Generic;

namespace HotelLibrary
{
    public class CreadorDeHabitaciones
    {

        public Habitacion CrearHabitacionSimple(int numero, 
                                                List<Artefacto> artefactos,
                                                List<Caracteristica> caracteristicas)
        {
            return new Habitacion("Habitacion simple", "Posee una cama de una plaza", 200, numero, artefactos, caracteristicas);
        }

        public Habitacion CrearHabitacionDobleMatrimonial(int numero,
                                                          List<Artefacto> artefactos,
                                                          List<Caracteristica> caracteristicas)
        {
            return new Habitacion("Habitacion doble matrimonial", "Posee una cama de dos plazas", 350, numero, artefactos, caracteristicas);
        }

        public Habitacion CrearHabitacionTriple(int numero,
                                                List<Artefacto> artefactos,
                                                List<Caracteristica> caracteristicas)
        {
            return new Habitacion("Habitacion triple", "Posee una cama matrimonial plus y una cama extra", 550, numero, artefactos, caracteristicas);
        }

        public Habitacion CrearHabitacionCuadruple(int numero,
                                                   List<Artefacto> artefactos,
                                                   List<Caracteristica> caracteristicas)
        {
            return new Habitacion("Habitacion cuádruple", "Posee una cama matrimonial y cama-cucheta.", 700, numero, artefactos, caracteristicas);
        }
    }
}