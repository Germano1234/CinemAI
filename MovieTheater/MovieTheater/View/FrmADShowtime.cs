using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieTheater.Model;
using MovieTheater.Repository;
using MovieTheater.Repository.Interfaces;

namespace MovieTheater.View
{
    public partial class FrmADShowtime : Form
    {
        public FrmADShowtime()
        {
            InitializeComponent();
            disableTxt();
        }
        Showtime showtime = new Showtime();
        RShowTime rShowtime = new RShowTime();

        private void enableTxt()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Enabled = true;
                }
            }
        }

        private void disableTxt()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Enabled = false;
                }
            }
            Add.Enabled = false;
            Update.Enabled = false;
            Delete.Enabled = false;
            txtShowtimeID.Enabled = true;
        }

        private void enableAddBtns()
        {
            Add.Enabled = true;
            Update.Enabled = false;
            Delete.Enabled = false;
            txtShowtimeID.Text = "";
        }

        private void enableUpDelBtns()
        {
            Add.Enabled = false;
            Update.Enabled = true;
            Delete.Enabled = true;
        }

        private bool checkForEmptyTB()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox tb)
                {
                    if (tb.Text == "" && tb != txtShowtimeID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void clearTxt()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Clear();
                }
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            enableTxt();
            enableAddBtns();
            clearTxt();
            showtime.ShowTimeID = -1;
            lbl.Text = "";
        }

        private void Search_Click(object sender, EventArgs e)
        {
            showtime = rShowtime.getByID(int.Parse(txtShowtimeID.Text));
            if (showtime == null)
            {
                MessageBox.Show("A Showtime with this ID does not exist.");
                disableTxt();
            }
            else
            {
                enableUpDelBtns();
                enableTxt();
                txtMovieID.Text = showtime.MovieID.ToString();
                txtRoomID.Text = showtime.RoomID.ToString();
                txtShowtime.Text = showtime.ShowTime;
                txtPrice.Text = showtime.Price.ToString();
                lbl.Text = "";
                showtime.ShowTimeID = int.Parse(txtShowtimeID.Text);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (checkForEmptyTB() == true)
            {
                lbl.Text = "Please fill all the fields";
            }
            else
            {
                showtime.MovieID = int.Parse(txtMovieID.Text);
                showtime.RoomID = int.Parse(txtRoomID.Text);
                showtime.ShowTime = txtShowtime.Text;
                showtime.Price = int.Parse(txtPrice.Text);
                if (rShowtime.CheckExistence(showtime) == true)
                {
                    MessageBox.Show("A showtime at this TIME at this ROOM already exists.");
                }
                else
                {
                    rShowtime.insert(showtime);
                    MessageBox.Show("Successfully added!");
                    disableTxt();
                }
                lbl.Text = "";
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (checkForEmptyTB() == true)
            {
                lbl.Text = "Please fill all the fields";
            }
            else
            {
                showtime.MovieID = int.Parse(txtMovieID.Text);
                showtime.RoomID = int.Parse(txtRoomID.Text);
                showtime.ShowTime = txtShowtime.Text;
                showtime.Price = int.Parse(txtPrice.Text);
                if (rShowtime.CheckExistence(showtime) == true)
                {
                    MessageBox.Show("A showtime at this TIME at this ROOM already exists.");
                }
                else
                {
                    rShowtime.update(showtime);
                    MessageBox.Show("Successfully updated!");
                    disableTxt();
                }
                lbl.Text = "";
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            rShowtime.delete(showtime.ShowTimeID);
            MessageBox.Show("Successfully deleted!");
            clearTxt();
            disableTxt();
        }
    }
}
