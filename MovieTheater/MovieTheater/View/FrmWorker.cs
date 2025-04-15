using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieTheater.View
{
    public partial class FrmWorker : Form
    {
        public FrmWorker()
        {
            InitializeComponent();
        }

        private void Room_Click(object sender, EventArgs e)
        {
            FrmADRoom frmADRoom = new FrmADRoom();
            frmADRoom.ShowDialog();
        }

        private void Movie_Click(object sender, EventArgs e)
        {
            FrmADMovie frmMovies = new FrmADMovie();
            frmMovies.ShowDialog();
        }

        private void Showtime_Click(object sender, EventArgs e)
        {
            FrmADShowtime frmADShowtime = new FrmADShowtime();
            frmADShowtime.ShowDialog();
        }

        private void Ticket_Click(object sender, EventArgs e)
        {
            FrmVDTicket frmVDTicket = new FrmVDTicket();
            frmVDTicket.ShowDialog();
        }

        private void View_Click(object sender, EventArgs e)
        {
            FrmViewAll frmViewAll = new FrmViewAll();
            frmViewAll.ShowDialog();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBase frmBase = new FrmBase();
            frmBase.ShowDialog();
        }
    }
}
