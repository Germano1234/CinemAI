using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Model;

namespace MovieTheater.Repository.Interfaces
{
    internal interface ITicket
    {
        IEnumerable<int> getSeatsTaken(int showtimeID);
        int InsertTicket(Ticket ticket);
    }
}
