using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Repository.Interfaces;
using MovieTheater.Model;
using Microsoft.Data.SqlClient;


namespace MovieTheater.Repository
{
    internal class RUser : IUser
    {
        public void insert(User user)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Insert into [User](Name, Email, Password, IsWorker) " +
                $"values ('{user.Name}', '{user.Email}', '{user.Password}', '{user.IsWorker}')";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public User get(string email, string password)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from [User] where Email = '{email}' and Password = '{password}'";
            SqlDataReader reader = cmd.ExecuteReader();

            User user = new User();
            if (reader.Read())
            {
                user.UserID = int.Parse(reader[0].ToString());
                user.Name = reader[1].ToString();
                user.Email = reader[2].ToString();
                user.Password = reader[3].ToString();
                user.IsWorker = reader[4].ToString();
            } else
            {
                return null;
            }

            connection.Close();
            return user;
        }

        public IEnumerable<User> getAllCustumers()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from [User] where IsWorker = 'no'";
            SqlDataReader reader = cmd.ExecuteReader();

            List<User> users = new List<User>();
            while (reader.Read())
            {
                User user = new User();
                user.UserID = int.Parse(reader[0].ToString());
                user.Name = reader[1].ToString();
                user.Email = reader[2].ToString();
                user.Password = reader[3].ToString();
                user.IsWorker = reader[4].ToString();
                users.Add(user);
            }

            connection.Close();
            return users;
        }

        public bool checkExistenceByID(int ID) // for custumer
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from [User] where UserID = {ID} and IsWorker = 'no'";
            SqlDataReader reader = cmd.ExecuteReader();

            bool answer;
            if (reader.Read())
            {
                answer = true;
            }
            else
            {
                answer = false;
            }

            connection.Close();
            return answer;
        }
    }
}
