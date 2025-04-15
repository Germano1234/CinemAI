using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieTheater.Repository;

namespace MovieTheater.View
{
    public partial class FrmViewAll : Form
    {
        public FrmViewAll()
        {
            InitializeComponent();
            button1.Enabled = false;
        }
        RRoom rRoom = new RRoom();
        RMovie rMovie = new RMovie();
        RShowTime rShowtime = new RShowTime();
        RUser rUser = new RUser();
        RTicket rTicket = new RTicket();

        private void Box_TextChanged(object sender, EventArgs e)
        {
            switch (Box.Text)
            {
                case "Room":
                    dataGridView.DataSource = rRoom.getAll();
                    break;
                case "Movie":
                    dataGridView.DataSource = rMovie.getAll();
                    break;
                case "Showtime":
                    dataGridView.DataSource = rShowtime.getAll();
                    break;
                case "Custumer":
                    dataGridView.DataSource = rUser.getAllCustumers();
                    break;
                case "Ticket":
                    textBox1.Enabled = true;
                    button1.Enabled = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rUser.checkExistenceByID(int.Parse(textBox1.Text)) == false)
            {
                MessageBox.Show("A Custumer with this ID does not exist.");
            } else
            {
                dataGridView.DataSource = rTicket.getAllForCustumer(int.Parse(textBox1.Text));
            }
            textBox1.Enabled=false;
            button1 .Enabled=false;
            Box.Text = "Choose again";
        }
    }
}
