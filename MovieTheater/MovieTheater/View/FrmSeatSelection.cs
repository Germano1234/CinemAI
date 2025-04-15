using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MovieTheater.Repository;

namespace MovieTheater.View
{
    public partial class FrmSeatSelection : Form
    {
        private Image availableSeatImage = Properties.Resources.Green;
        private Image takenSeatImage = Properties.Resources.Red;
        private Image selectedImage = Properties.Resources.yellow;
        private RTicket ticketRepository = new RTicket();  // Access database
        private List<int> seats = new List<int>();

        public FrmSeatSelection()
        {
            InitializeComponent();
            LoadSeats();
        }

        private void LoadSeats()
        {
            // Rigth now same room view for all rooms but room view based on capacity still to be implemented
            int rows = 5;
            int cols = 10;
            int seatSize = 40;
            int spacing = 10;
            int startX = 120;
            int startY = 130;

            // 🔴 1. Get taken seats from the database
            HashSet<int> takenSeats = new HashSet<int>(ticketRepository.getSeatsTaken(FrmShowtime.showtime.ShowTimeID));

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int seatNumber = row * cols + col + 1; // Unique seat number

                    PictureBox seatPictureBox = new PictureBox
                    {
                        Width = seatSize,
                        Height = seatSize,
                        Image = availableSeatImage, // Default: Green
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = seatNumber,  // Store seat number
                        Location = new Point(startX + col * (seatSize + spacing), startY + row * (seatSize + spacing))
                    };

                    // 🔴 2. Check if the seat is taken, mark it red if so
                    if (takenSeats.Contains(seatNumber))
                    {
                        seatPictureBox.Image = takenSeatImage; // Taken seats turn Red
                    }
                    else
                    {
                        seatPictureBox.Click += Seat_Click;
                    }

                    Controls.Add(seatPictureBox);
                }
            }
        }

        private void Seat_Click(object sender, EventArgs e)
        {
            PictureBox seat = sender as PictureBox;
            int seatNumber = (int)seat.Tag; // Get seat number
            if (seats.Contains(seatNumber))
            {
                seat.Image = availableSeatImage;
                seats.Remove(seatNumber);
            } else
            {
                seats.Add(seatNumber);
                seat.Image = selectedImage;
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmShowtime frmShowtime = new FrmShowtime();
            frmShowtime.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCheckout frmCheckout = new FrmCheckout(seats);
            frmCheckout.ShowDialog();
        }
    }
}


