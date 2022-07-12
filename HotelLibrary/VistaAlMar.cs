namespace HotelLibrary
{
    public class VistaAlMar : Caracteristica
    {
        public VistaAlMar() : base("Vista al mar")
        {
        }

        public override float AplicarRecargo(float precioBase)
        {
            return precioBase + ((precioBase * 15) / 100);
        }
    }
}