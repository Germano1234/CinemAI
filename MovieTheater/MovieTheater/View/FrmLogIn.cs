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
    public partial class FrmLogIn : Form
    {
        public FrmLogIn()
        {
            InitializeComponent();
            string name = "";
            int idForTicket = 0;
        }
        User user = new User();
        RUser Ruser = new RUser();
        public static User logged;

        private void Login_Click(object sender, EventArgs e)
        {
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Text;

            logged = Ruser.get(user.Email, user.Password);
            if (logged != null)
            {
                if (logged.IsWorker == "yes")
                {
                    this.Hide();
                    FrmWorker frmWorker = new FrmWorker();
                    frmWorker.Show();
                }
                else
                {
                    this.Hide();
                    FrmMovies frmMovies = new FrmMovies();
                    frmMovies.ShowDialog();
                }
            }
            else
            {
                InvalidCred.Text = "Invalid Credentials";
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBase frmBase = new FrmBase();
            frmBase.ShowDialog();
        }

    }
}
