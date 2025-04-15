using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Model
{
    internal class Movie
    {
        private int movieID;
        private string title;
        private string description;
        private int duration;
        private string restriction;
        private float rating;
        private string genre;

        public int MovieID { get => movieID; set => movieID = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public int Duration { get => duration; set => duration = value; }
        public string Restriction { get => restriction; set => restriction = value; }
        public float Rating { get => rating; set => rating = value; }
        public string Genre { get => genre; set => genre = value; }
    }
}
