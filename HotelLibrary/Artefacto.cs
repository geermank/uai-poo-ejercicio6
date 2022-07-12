namespace HotelLibrary
{
    public class Artefacto
    {
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
        }

        public Artefacto(string nombre)
        {
            this.nombre = nombre;
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}