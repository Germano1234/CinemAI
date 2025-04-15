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
    public partial class FrmSignUp : Form
    {
        public FrmSignUp()
        {
            InitializeComponent();
        }

        User user = new User();
        RUser Ruser = new RUser();

        private void SignIn_Click(object sender, EventArgs e)
        {
            user.Name = txtName.Text;
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Text;

            if (txtCode.Text == "")
            {
                user.IsWorker = "no";
                Ruser.insert(user);
                MessageBox.Show("Registered User!");
                this.Hide();
                FrmBase b = new FrmBase();
                b.ShowDialog();
            }
            else if (txtCode.Text == "worker")
            {
                user.IsWorker = "yes";
                Ruser.insert(user);
                MessageBox.Show("Registered Worker!");
                this.Hide();
                FrmBase b = new FrmBase();
                b.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong code for worker, leave in blank if custumer.");
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
