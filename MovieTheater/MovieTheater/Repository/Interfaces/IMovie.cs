using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Model;

namespace MovieTheater.Repository.Interfaces
{
    internal interface IMovie
    {
        IEnumerable<Movie> getAll();

        Movie getByID(int id);
    }
}
