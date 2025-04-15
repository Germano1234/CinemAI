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
    internal class RTicket : ITicket
    {
        public IEnumerable<int> getSeatsTaken(int showtimeID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select SeatNumber from Ticket where ShowtimeID = {showtimeID}";
            SqlDataReader reader = cmd.ExecuteReader();

            List<int> list = new List<int>();
            while (reader.Read())
            {
                list.Add(int.Parse(reader[0].ToString()));
            }

            connection.Close();
            return list;
        }

        public int InsertTicket(Ticket ticket)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = $"INSERT INTO Ticket (CustumerID, ShowtimeID, SeatNumber, Price) " +
                              $"VALUES ({ticket.CustumerID}, {ticket.ShowtimeID}, '{ticket.SeatNumber}', {ticket.Price}); " +
                              "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int insertedID = (int)cmd.ExecuteScalar(); // 🔥 Gets the TicketID
            connection.Close();

            return insertedID;
        }

        public Ticket getTicketByID(int ticketID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from Ticket where TicketID = {ticketID}";
            SqlDataReader reader = cmd.ExecuteReader();

            Ticket ticket = new Ticket();
            if (reader.Read())
            {
                ticket.CustumerID = int.Parse(reader[1].ToString());
                ticket.ShowtimeID = int.Parse(reader[2].ToString());
                ticket.SeatNumber = int.Parse(reader[3].ToString());
                ticket.Price = float.Parse(reader[4].ToString());
            } else
            {
                ticket = null;
            }

            connection.Close();
            return ticket;
        }

        public void delete(int ticketID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"DELETE FROM Ticket WHERE TicketID = {ticketID}";

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public IEnumerable<Ticket> getAllForCustumer(int ID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from Ticket where CustumerID = {ID}";
            SqlDataReader reader = cmd.ExecuteReader();

            List<Ticket> tickets = new List<Ticket>();
            while (reader.Read())
            {
                Ticket ticket = new Ticket();
                ticket.TicketID = int.Parse(reader[0].ToString());
                ticket.CustumerID = int.Parse(reader[1].ToString());
                ticket.ShowtimeID = int.Parse(reader[2].ToString());
                ticket.SeatNumber = int.Parse(reader[3].ToString());
                ticket.Price = float.Parse(reader[4].ToString());
                tickets.Add(ticket);
            }

            connection.Close();
            return tickets;
        }

        public List<Movie> GetWatchedMoviesByUserId(int userId)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = @"
                SELECT DISTINCT m.movieID, m.title, m.genre
                FROM Ticket t
                JOIN Showtime s ON t.showtimeID = s.showtimeID
                JOIN Movie m ON s.movieID = m.movieID
                WHERE t.custumerID = @userId";

            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Movie> watchedMovies = new List<Movie>();
            while (reader.Read())
            {
                Movie movie = new Movie();
                movie.MovieID = int.Parse(reader[0].ToString());
                movie.Title = reader[1].ToString();
                movie.Genre = reader[2].ToString();
                watchedMovies.Add(movie);
            }

            connection.Close();
            return watchedMovies;
        }


    }
}
