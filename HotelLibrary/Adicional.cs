namespace HotelLibrary
{
    public class Adicional
    {
        protected string nombre;
        protected float precio;

        public Adicional(string nombre, float precio)
        {
            this.nombre = nombre;
            this.precio = precio;
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public virtual float Precio(int cantidadDeNoches)
        {
            return precio * cantidadDeNoches;
        }
    }
}