namespace HotelLibrary
{
    public abstract class Caracteristica
    {
        private string nombre;

        public Caracteristica(string nombre)
        {
            this.nombre = nombre;
        }

        public abstract float AplicarRecargo(float precioBase);

        public override string ToString()
        {
            return nombre;
        }
    }
}