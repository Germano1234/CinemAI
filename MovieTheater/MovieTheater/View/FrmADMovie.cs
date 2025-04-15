using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieTheater.Model;
using MovieTheater.Repository;
using MovieTheater.Repository.Interfaces;

namespace MovieTheater.View
{
    public partial class FrmADMovie : Form
    {
        public FrmADMovie()
        {
            InitializeComponent();
            disableTxt();
        }
        Movie movie = new Movie();
        RMovie rMovie = new RMovie();

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
            txtMovieID.Enabled = true;
        }

        private void enableAddBtns()
        {
            Add.Enabled = true;
            Update.Enabled = false;
            Delete.Enabled = false;
            txtMovieID.Text = "";
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
                    if (tb.Text == "" && tb != txtMovieID)
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
            lbl.Text = "";
            movie.MovieID = -1;
        }

        private void Search_Click(object sender, EventArgs e)
        {
            movie = rMovie.getByID(int.Parse(txtMovieID.Text));
            if (movie == null)
            {
                MessageBox.Show("A room with this ID does not exist.");
                disableTxt();
            }
            else
            {
                enableUpDelBtns();
                enableTxt();
                txtTitle.Text = movie.Title;
                txtDuration.Text = movie.Duration.ToString();
                txtRestriction.Text = movie.Restriction;
                txtDescription.Text = movie.Description;
                txtRating.Text = movie.Rating.ToString();
                txtGenre.Text = movie.Genre;
                lbl.Text = "";
                movie.MovieID = int.Parse(txtMovieID.Text);
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
                movie.Title = txtTitle.Text;
                movie.Duration = int.Parse(txtDuration.Text);
                movie.Restriction = txtRestriction.Text;
                movie.Description = txtDescription.Text;
                movie.Rating = float.Parse(txtRating.Text);
                movie.Genre = txtGenre.Text;
                if (rMovie.CheckExistence(movie) == true)
                {
                    MessageBox.Show("A movie with this Title already exists.");
                }
                else
                {
                    rMovie.insert(movie);
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
                movie.Title = txtTitle.Text;
                movie.Duration = int.Parse(txtDuration.Text);
                movie.Restriction = txtRestriction.Text;
                movie.Description = txtDescription.Text;
                movie.Rating = float.Parse(txtRating.Text);
                movie.Genre = txtGenre.Text;
                if (rMovie.CheckExistence(movie) == true)
                {
                    MessageBox.Show("A movie with this Title already exists.");
                }
                else
                {
                    rMovie.update(movie);
                    MessageBox.Show("Successfully updated!");
                    disableTxt();
                }
                lbl.Text = "";
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            rMovie.delete(movie.MovieID);
            MessageBox.Show("Successfully deleted!");
            clearTxt();
            disableTxt();
        }
    }
}
