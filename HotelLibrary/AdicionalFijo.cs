namespace HotelLibrary
{
    public class AdicionalFijo : Adicional
    {
        public AdicionalFijo(string nombre, float precio) : base(nombre, precio)
        {
        }

        public override float Precio(int cantidadDeNoches)
        {
            return precio;
        }
    }
}