using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Model
{
    public class Showtime
    {
        private int showTimeID;
        private int movieID;
        private int roomID;
        private string showTime;
        private float price;

        public int ShowTimeID { get => showTimeID; set => showTimeID = value; }
        public int MovieID { get => movieID; set => movieID = value; }
        public int RoomID { get => roomID; set => roomID = value; }
        public string ShowTime { get => showTime; set => showTime = value; }
        public float Price { get => price; set => price = value; }
    }
}
