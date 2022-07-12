using System.Collections.Generic;
using System.Linq;

namespace HotelLibrary
{
    public class Estadia
    {
        private Reserva reserva;
        private List<Adicional> adicionalesGeneradosEnLaEstadia;

        public Estadia(Reserva reserva)
        {
            this.reserva = reserva;
        }

        public void RegistrarAdicional(Adicional adicional)
        {
            adicionalesGeneradosEnLaEstadia.Add(adicional);
        }

        public float ImporteAPagar
        {
            get
            {
                return reserva.ImportePendienteDePago + CostoAdicionales();
            }
        }

        public float ImporteTotalFacturado
        {
            get
            {
                return ImporteAPagar + reserva.Deposito;
            }
        }

        private float CostoAdicionales()
        {
            return adicionalesGeneradosEnLaEstadia.Sum(a => a.Precio(reserva.CantidadDeNoches));
        }
    }
}