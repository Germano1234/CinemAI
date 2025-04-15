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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace MovieTheater.View
{
    public partial class FrmCheckout : Form
    {
        private List<int> seats;
        private Ticket ticket = new Ticket();
        private RMovie Rmovie = new RMovie();
        Movie movie;
        RTicket Rticket = new RTicket();

        public FrmCheckout(List<int> seats)
        {
            this.seats = seats;
            double price = 0;
            string seatsString = "";
            foreach (int seat in seats)
            {
                price += (FrmShowtime.showtime.Price * 1.1);
                seatsString += seat.ToString() + ", ";
            }
            seatsString = seatsString.Substring(0, seatsString.Length - 2); // take out the last , 
            movie = Rmovie.getByID(FrmShowtime.showtime.MovieID);
            InitializeComponent();
            lblName.Text = $"Hi {FrmLogIn.logged.Name}, Id = {FrmLogIn.logged.UserID}";
            lblCheckout.Text = $"Are you sure you want to buy this ticket: \n\n" +
                $"Movie: {movie.Title}\n" +
                $"Duration: {movie.Duration} minutes\n" +
                $"Restriction: {movie.Restriction}\n" +
                $"Room: {FrmShowtime.room.RoomNumber}\n" +
                $"Time: {FrmShowtime.showtime.ShowTime}\n" +
                $"Seat(s): " + seatsString + "\n" +
                $"3D: {FrmShowtime.room.Is3D}\n" +
                $"Price: ${(price):F2}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (int seat in seats)
            {
                ticket.CustumerID = FrmLogIn.logged.UserID;
                ticket.ShowtimeID = FrmShowtime.showtime.ShowTimeID;
                ticket.SeatNumber = seat;
                ticket.Price = FrmShowtime.showtime.Price * (float)1.1;
                ticket.TicketID = Rticket.InsertTicket(ticket);
                ExportTicketToPDF(ticket);
            }
            this.Hide();
            FrmMovies frmMovies = new FrmMovies();
            frmMovies.ShowDialog();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSeatSelection frmSeatSelection = new FrmSeatSelection();
            frmSeatSelection.ShowDialog();

        }

        private void ExportTicketToPDF(Ticket ticket)
        {
            Document doc = new Document();
            string fileName = $"Ticket_{ticket.TicketID}.pdf";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();

            // Add ticket info to the PDF
            doc.Add(new Paragraph("Movie Ticket"));
            doc.Add(new Paragraph("-----------------------------"));
            doc.Add(new Paragraph($"Ticket ID: {ticket.TicketID}"));
            doc.Add(new Paragraph($"Customer ID: {ticket.CustumerID}"));
            doc.Add(new Paragraph($"Showtime ID: {ticket.ShowtimeID}"));
            doc.Add(new Paragraph($"Seat Number: {ticket.SeatNumber}"));
            doc.Add(new Paragraph($"Price: ${ticket.Price:F2}"));
            doc.Add(new Paragraph($"Date Issued: {DateTime.Now}"));

            doc.Close();

            MessageBox.Show($"Ticket saved to Desktop as:\n{fileName}");
        }
    }
}
