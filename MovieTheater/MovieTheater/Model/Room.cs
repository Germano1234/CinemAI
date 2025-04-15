using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Model
{
    public class Room
    {
        private int roomID;
        private int roomNumber;
        private int capacity;
        private string is3D;

        public int RoomID { get => roomID; set => roomID = value; }
        public int RoomNumber { get => roomNumber; set => roomNumber = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public string Is3D { get => is3D; set => is3D = value; }
    }
}
