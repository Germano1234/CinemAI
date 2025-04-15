using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Model;

namespace MovieTheater.Repository.Interfaces
{
    internal interface IShowtime
    {
        IEnumerable<Showtime> getByRoomAndMovie(int roomID, int movieID);
        Showtime getByID(int id);
    }
}
