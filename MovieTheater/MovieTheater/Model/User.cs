using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Model
{
    public class User
    {
        private int userID;
        private string name;
        private string email;
        private string password;
        private string isWorker;

        public int UserID { get => userID; set => userID = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string IsWorker { get => isWorker; set => isWorker = value; }
    }
}
