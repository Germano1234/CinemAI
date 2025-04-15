using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Model;
using MovieTheater.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace MovieTheater.Repository
{
    internal class RShowTime : IShowtime
    {
        public IEnumerable<Showtime> getByRoomAndMovie(int roomID, int movieID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from ShowTime where MovieID = {movieID} and RoomID = {roomID}";
            SqlDataReader reader = cmd.ExecuteReader();

            List<Showtime> list = new List<Showtime>();
            while (reader.Read())
            {
                Showtime showtime = new Showtime();
                showtime.ShowTimeID = int.Parse(reader[0].ToString());
                showtime.MovieID = int.Parse(reader[1].ToString());
                showtime.RoomID = int.Parse(reader[2].ToString());
                showtime.ShowTime = reader[3].ToString();
                showtime.Price = float.Parse(reader[4].ToString());
                list.Add(showtime);
            }

            connection.Close();
            return list;
        }

        public Showtime getByID(int id)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from ShowTime where ShowtimeID = {id}";
            SqlDataReader reader = cmd.ExecuteReader();

            Showtime showtime = new Showtime();
            if (reader.Read())
            {
                showtime.ShowTimeID = int.Parse(reader[0].ToString());
                showtime.MovieID = int.Parse(reader[1].ToString());
                showtime.RoomID = int.Parse(reader[2].ToString());
                showtime.ShowTime = reader[3].ToString();
                showtime.Price = float.Parse(reader[4].ToString());
            } else
            {
                showtime = null;
            }

            connection.Close();
            return showtime;
        }

        public IEnumerable<Showtime> getAll()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from ShowTime";
            SqlDataReader reader = cmd.ExecuteReader();

            List<Showtime> list = new List<Showtime>();
            while (reader.Read())
            {
                Showtime showtime = new Showtime();
                showtime.ShowTimeID = int.Parse(reader[0].ToString());
                showtime.MovieID = int.Parse(reader[1].ToString());
                showtime.RoomID = int.Parse(reader[2].ToString());
                showtime.ShowTime = reader[3].ToString();
                showtime.Price = float.Parse(reader[4].ToString());
                list.Add(showtime);
            }

            connection.Close();
            return list;
        }


        public void insert(Showtime showtime)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Insert into ShowTime(MovieID, RoomID, ShowTime, Price) " +
                $"values ({showtime.MovieID}, {showtime.RoomID}, '{showtime.ShowTime}', {showtime.Price})";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public bool CheckExistence(Showtime showtime)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select ShowtimeID from ShowTime where ShowTime = '{showtime.ShowTime}' and RoomID = {showtime.RoomID}";
            SqlDataReader reader = cmd.ExecuteReader();

            bool answer;
            if (reader.Read())
            {
                if (int.Parse(reader[0].ToString()) == showtime.ShowTimeID)
                {
                    answer = false;
                }
                else
                {
                    answer = true;
                }
            }
            else
            {
                answer = false;
            }

            connection.Close();
            return answer;
        }

        public void update(Showtime showtime)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"UPDATE Showtime SET MovieID = {showtime.MovieID}, RoomID = {showtime.RoomID}, " +
                $"ShowTime = '{showtime.ShowTime}', Price = {showtime.Price}" +
                              $"WHERE ShowTimeID = {showtime.ShowTimeID}";

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void delete(int showtimeID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"DELETE FROM ShowTime WHERE ShowTimeID = {showtimeID}";

            cmd.ExecuteNonQuery();
            connection.Close();
        }

    }
}

