using System;

namespace HotelLibrary
{
    public class Persona
    {
        private int dni;
        private string nombre;
        private string apellido;
        private DateTime fechaNacimiento;

        public Persona(int dni, string nombre, string apellido, DateTime fechaNacimiento)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.fechaNacimiento = fechaNacimiento;
        }

        public int Dni
        {
            get { return dni; }
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public string Apellido
        {
            get { return apellido; }
        }

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
        }
    }
}