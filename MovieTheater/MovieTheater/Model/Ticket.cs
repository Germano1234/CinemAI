using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Model
{
    internal class Ticket
    {
        private int ticketID;
        private int custumerID;
        private int showtimeID;
        private int seatNumber;
        private float price;

        public int TicketID { get => ticketID; set => ticketID = value; }
        public int CustumerID { get => custumerID; set => custumerID = value; }
        public int ShowtimeID { get => showtimeID; set => showtimeID = value; }
        public int SeatNumber { get => seatNumber; set => seatNumber = value; }
        public float Price { get => price; set => price = value; }
    }
}
