using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Model;

namespace MovieTheater.Repository.Interfaces
{
    internal interface IUser
    {
        void insert(User user);
        User get(string email,  string password);
    }
}
