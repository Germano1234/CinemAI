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
    public partial class FrmVDTicket : Form
    {
        public FrmVDTicket()
        {
            InitializeComponent();
            disableTxt();
        }
        Ticket ticket = new Ticket();
        RTicket rTicket = new RTicket();
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
        private void disableTxt()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Enabled = false;
                }
            }
            Delete.Enabled = false;
            txtTicketID.Enabled = true;
        }

        private void Search_Click(object sender, EventArgs e)
        {
            ticket = rTicket.getTicketByID(int.Parse(txtTicketID.Text));
            if (ticket == null)
            {
                MessageBox.Show("A Ticket with this ID does not exist.");
                clearTxt();
                disableTxt();
            }
            else
            {
                Delete.Enabled = true;
                txtCustumerID.Text = ticket.CustumerID.ToString();
                txtShowtimeID.Text = ticket.ShowtimeID.ToString();
                txtSeatNumber.Text = ticket.SeatNumber.ToString();
                txtPrice.Text = ticket.Price.ToString();
                ticket.TicketID = int.Parse(txtTicketID.Text);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            rTicket.delete(ticket.TicketID);
            MessageBox.Show("Successfully deleted!");
            clearTxt();
            Delete.Enabled = false;
        }
    }
}
