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

namespace MovieTheater.View
{
    public partial class FrmViewMyTicket : Form
    {
        private RTicket Rticket = new RTicket();
        private Ticket ticket = new Ticket();
        public FrmViewMyTicket()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            ticket = Rticket.getTicketByID(int.Parse(txtTicketID.Text));
            if (ticket == null)
            {
                MessageBox.Show("Ticket does not exist.");
                return;
            }
            if (ticket.CustumerID == FrmLogIn.logged.UserID)
            {
                lblCheckout.Text = $"Ticket: \n\n" +
                $"Custumer ID: {ticket.CustumerID}\n" +
                $"Showtime ID: {ticket.ShowtimeID}\n" +
                $"Seat Number: {ticket.SeatNumber}\n" +
                $"Price: {ticket.Price:F2}\n";
            } else
            {
                MessageBox.Show("This ticked do not belong to the custumer logged in.");
            }
            
        }
    }
}
