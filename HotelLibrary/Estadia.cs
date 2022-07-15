using System.Collections.Generic;
using System.Linq;

namespace HotelLibrary
{
    public class Estadia
    {
        private Reserva reserva;
        private List<Adicional> adicionalesGeneradosEnLaEstadia;
        private bool concluida;

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

        public bool Concluida
        {
            get { return concluida; }
        }
        
        public Reserva Reserva
        {
            get { return reserva; }
        }
        
        public void Concluir()
        {
            concluida = true;
        }

        private float CostoAdicionales()
        {
            return adicionalesGeneradosEnLaEstadia.Sum(a => a.Precio(reserva.CantidadDeNoches));
        }
    }
}