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
    public partial class FrmShowtime : Form
    {
        public FrmShowtime()
        {
            InitializeComponent();
            LoadShowtimes();

        }

        private int roomID;
        int movieID = FrmMovies.idChosenMovie;
        RShowTime Rshowtime = new RShowTime();
        RMovie Rmovie = new RMovie();
        RRoom Rroom = new RRoom();
        public static Showtime showtime = new Showtime();
        public static Room room = new Room();

        private void LoadShowtimes()
        {
            Movie movie = new Movie();
            movie = Rmovie.getByID(movieID);
            lblMovieName.Text = movie.Title;
            lblMovieDescription.Text = movie.Description;
            lblGenre.Text = movie.Genre;
            showtimePanel.Controls.Clear();
            List<Room> rooms = Rroom.getAll().ToList();
            int y = 0;

            foreach (Room room in rooms)
            {
                Label roomLabel = new Label
                {
                    Text = $"Room {room.RoomNumber}",
                    AutoSize = true,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Location = new Point(0, y)
                };
                showtimePanel.Controls.Add(roomLabel);
                y += 30;

                List<Showtime> showtimes = Rshowtime.getByRoomAndMovie(room.RoomID, movieID).ToList();
                int x = 10;

                foreach (Showtime showtime in showtimes)
                {
                    Button showtimeButton = new Button
                    {
                        Text = $"{showtime.ShowTime} - ${showtime.Price}",
                        Width = 150,
                        Height = 40,
                        Location = new Point(x, y),
                        Tag = showtime.ShowTimeID,
                        Name = showtime.RoomID.ToString()
                    };

                    showtimeButton.Click += ShowtimeButton_Click;
                    showtimePanel.Controls.Add(showtimeButton);
                    x += 160;
                }

                y += 50;
            }
        }

        private void ShowtimeButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            showtime = Rshowtime.getByID((int)clickedButton.Tag);
            roomID = int.Parse(clickedButton.Name);
            room = Rroom.getById(roomID);
            this.Hide();
            FrmSeatSelection seatSelection = new FrmSeatSelection();
            seatSelection.ShowDialog();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmMovies frmMovies = new FrmMovies();
            frmMovies.ShowDialog();
        }
    }
}
