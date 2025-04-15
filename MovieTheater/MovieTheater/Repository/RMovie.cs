using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Repository.Interfaces;
using MovieTheater.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Microsoft.Data.SqlClient;

namespace MovieTheater.Repository
{
    internal class RMovie : IMovie
    {
        public IEnumerable<Movie> getAll()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from Movie";
            SqlDataReader reader = cmd.ExecuteReader();

            List<Movie> list = new List<Movie>();   
            while (reader.Read())
            {
                Movie movie = new Movie();
                movie.MovieID = int.Parse(reader[0].ToString());
                movie.Title = reader[1].ToString();
                movie.Duration = int.Parse(reader[2].ToString());
                movie.Restriction = reader[3].ToString();
                movie.Description = reader[4].ToString();
                movie.Rating = float.Parse(reader[5].ToString());
                movie.Genre = reader[6].ToString();
                list.Add(movie);
            }

            connection.Close();
            return list;
        }

        public Movie getByID(int id)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from Movie where MovieID = {id}";
            SqlDataReader reader = cmd.ExecuteReader();

            Movie movie = new Movie();
            if (reader.Read())
            {                
                movie.MovieID = int.Parse(reader[0].ToString());
                movie.Title = reader[1].ToString();
                movie.Duration = int.Parse(reader[2].ToString());
                movie.Restriction = reader[3].ToString();
                movie.Description = reader[4].ToString();
                movie.Rating = float.Parse(reader[5].ToString());
                movie.Genre = reader[6].ToString();
            } else
            {
                movie = null;
            }

            connection.Close();
            return movie;
        }

        public bool CheckExistence(Movie movie)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select MovieID from Movie where Title = '{movie.Title}'";
            SqlDataReader reader = cmd.ExecuteReader();

            bool answer;
            if (reader.Read())
            {
                if (int.Parse(reader[0].ToString()) == movie.MovieID)
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

        public void insert(Movie movie)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Insert into Movie(Title, Duration, Restriction, Description, Rating, Genre) " +
                $"values ('{movie.Title}', {movie.Duration}, '{movie.Restriction}', " +
                $"'{movie.Description}', {movie.Rating}, '{movie.Genre}')";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void update(Movie movie)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"UPDATE Movie SET Title = '{movie.Title}', Duration = {movie.Duration}, " +
                $"Restriction = '{movie.Restriction}', Description = '{movie.Description}', Rating = {movie.Rating}," +
                $"Genre = '{movie.Genre}' WHERE MovieID = {movie.MovieID}";

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void delete(int movieID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"DELETE FROM Movie WHERE MovieID = {movieID}";

            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
