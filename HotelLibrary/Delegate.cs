using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary
{
    public class Delegate
    {
        public delegate void delExitoEnReserva(Reserva reserva);
        public delegate void delErrorEnReserva(string error);
    }
}
