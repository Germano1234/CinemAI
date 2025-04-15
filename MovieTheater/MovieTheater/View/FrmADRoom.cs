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
    public partial class FrmADRoom : Form
    {
        public FrmADRoom()
        {
            InitializeComponent();
            disableTxt();
        }
        Room room = new Room();
        RRoom rRoom = new RRoom();

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
            txtRoomID.Enabled = true;
        }

        private void enableAddBtns()
        {
            Add.Enabled = true;
            Update.Enabled = false;
            Delete.Enabled = false;
            txtRoomID.Text = "";
        }

        private void enableUpDelBtns()
        {
            Add.Enabled = false;
            Update.Enabled = true;
            Delete.Enabled = true;
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
            room.RoomID = -1;
        }

        private void Search_Click(object sender, EventArgs e)
        {
            room = rRoom.getById(int.Parse(txtRoomID.Text));
            if (room == null)
            {
                MessageBox.Show("A room with this ID does not exist.");
                disableTxt();
            }
            else
            {
                enableUpDelBtns();
                enableTxt();
                txtRoomNumber.Text = room.RoomNumber.ToString();
                txtCapacity.Text = room.Capacity.ToString();
                txt3D.Text = room.Is3D;
                lbl.Text = "";
                room.RoomID = int.Parse(txtRoomID.Text);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (txtRoomNumber.Text == "" || txtCapacity.Text == "" || txt3D.Text == "")
            {
                lbl.Text = "Please fill all the fields";
            } else
            {
                room.RoomNumber = int.Parse(txtRoomNumber.Text);
                room.Capacity = int.Parse(txtCapacity.Text);
                room.Is3D = txt3D.Text;
                if (rRoom.CheckExistence(room) == true)
                {
                    MessageBox.Show("This room already exists.");
                }
                else
                {
                    rRoom.insert(room);
                    MessageBox.Show("Successfully added!");
                    disableTxt();
                }
                lbl.Text = "";
                
            }
            
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (txtRoomNumber.Text == "" || txtCapacity.Text == "" || txt3D.Text == "")
            {
                lbl.Text = "Please fill all the fields";
            }
            else
            {
                room.RoomNumber = int.Parse(txtRoomNumber.Text);
                room.Capacity = int.Parse(txtCapacity.Text);
                room.Is3D = txt3D.Text;
                if (rRoom.CheckExistence(room) == true)
                {
                    MessageBox.Show("This room already exists.");
                }
                else
                {
                    rRoom.update(room);
                    MessageBox.Show("Successfully updated!");
                    disableTxt();
                }
                lbl.Text = "";
                
            }
            
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            rRoom.delete(room.RoomID);
            MessageBox.Show("Successfully deleted!");
            enableAddBtns();
            clearTxt();
            disableTxt();
        }
    }
}
