using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTheater.Model;
using Microsoft.Data.SqlClient;
using MovieTheater.Repository.Interfaces;
using Microsoft.VisualBasic.ApplicationServices;

namespace MovieTheater.Repository
{
    internal class RRoom : IRoom
    {
        public IEnumerable<Room> getAll()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from Room";
            SqlDataReader reader = cmd.ExecuteReader();

            List<Room> list = new List<Room>();
            while (reader.Read())
            {
                Room room = new Room();
                room.RoomID = int.Parse(reader[0].ToString());
                room.RoomNumber = int.Parse(reader[1].ToString());
                room.Capacity = int.Parse(reader[2].ToString());
                room.Is3D = reader[3].ToString();
                list.Add(room);
            }

            connection.Close();
            return list;
        }

        public Room getById(int id)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select * from Room where RoomID = {id}";
            SqlDataReader reader = cmd.ExecuteReader();

            Room room = new Room();
            if (reader.Read())
            {
                room.RoomID = int.Parse(reader[0].ToString());
                room.RoomNumber = int.Parse(reader[1].ToString());
                room.Capacity = int.Parse(reader[2].ToString());
                room.Is3D = reader[3].ToString();
            } else
            {
                room = null;
            }

            connection.Close();
            return room;
        }

        public void insert(Room room) {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Insert into Room(RoomNumber, Capacity, Is3D) " +
                $"values ('{room.RoomNumber}', '{room.Capacity}', '{room.Is3D}')";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public bool CheckExistence(Room room) 
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select RoomID from Room where RoomNumber = {room.RoomNumber}";
            SqlDataReader reader = cmd.ExecuteReader();

            bool answer;
            if (reader.Read())
            {
                if (int.Parse(reader[0].ToString()) == room.RoomID)
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

        public void update(Room room)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"UPDATE Room SET RoomNumber = {room.RoomNumber}, Capacity = {room.Capacity}, Is3D = '{room.Is3D}' " +
                              $"WHERE RoomID = {room.RoomID}";

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void delete(int roomID)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connString;
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"DELETE FROM Room WHERE RoomID = {roomID}";

            cmd.ExecuteNonQuery();
            connection.Close();
        }

    }
}
