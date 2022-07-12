using System.Collections.Generic;

namespace HotelLibrary
{
    public class CreadorDeAdicionales
    {
        public const string CUNA = "Cuna";
        public const string FRIGOBAR = "Consumo frigobar";

        public List<string> AdicionalesDisponibles()
        {
            return new List<string>
            {
                CUNA,
                FRIGOBAR
            };
        }

        public Adicional CrearAdicionalCuna()
        {
            return new Adicional("Cuna", 50);
        }

        public Adicional CrearConsumoFrigobar(float importe)
        {
            return new AdicionalFijo("Consumo frigobar", importe);
        }
    }
}