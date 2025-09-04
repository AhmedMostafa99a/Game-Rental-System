using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Repositories;

namespace GameRentalSystem
{
    public partial class Signup : Form
    {
        bool flag = false;
        public Signup()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }
        private void sign_up_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password.Text) || string.IsNullOrWhiteSpace(Email.Text) ||
       string.IsNullOrWhiteSpace(UserName.Text) || string.IsNullOrWhiteSpace(Address.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password.Text.Length != 0 && Email.Text.Length != 0
            && UserName.Text.Length != 0 && Address.Text.Length != 0)
            {
                if (admin.Checked)
                {
                    flag = true;
                    AdminRepository adminRepo = new AdminRepository();
                    Admin new_admin = new Admin();
                    new_admin.Password = password.Text;
                    new_admin.Email = Email.Text;
                    new_admin.AdminName = UserName.Text;
                    if (adminRepo.AddAdmin(new_admin))
                    {
                        MessageBox.Show("Admin added successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add");
                    }
                }
                else
                {
                    UserRepository userRepo = new UserRepository();
                    User new_User = new User();
                    new_User.UserName = UserName.Text;
                    new_User.Password = password.Text;
                    new_User.Email = Email.Text;
                    new_User.Address = Address.Text;
                    if (userRepo.AddUser(new_User))
                    {
                        MessageBox.Show("User added successfully.");
                    }
                    else
                    {
                        MessageBox.Show("User already exists.");

                    }
                }
            }
            Loginform login = new Loginform(flag);
            this.Hide();
            login.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void admin_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

