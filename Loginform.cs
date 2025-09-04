using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Repositories;


namespace GameRentalSystem
{
    public partial class Loginform : Form
    {
        private bool flag;
        private AdminRepository _adminRepository;
        private UserRepository _userRepository;


        public Loginform(bool f)
        {
            this.flag = f;
            InitializeComponent();
            _adminRepository = new AdminRepository();
            _userRepository = new UserRepository();
        }

        private void Loginform_Load(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void CenterControls()
        {
            // Arrange controls vertically centered
            int spacing = 10;
            int totalHeight = txtUsername.Height + txtPassword.Height + btnLogin.Height + (spacing * 3);
            int startY = (this.ClientSize.Height - totalHeight) / 2;

            int centerX = (this.ClientSize.Width - txtUsername.Width) / 2;

            txtUsername.Location = new System.Drawing.Point(centerX, startY);
            txtPassword.Location = new System.Drawing.Point(centerX, txtUsername.Bottom + spacing);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Try to log in as Admin first
            Admin admin = _adminRepository.GetAdminByAdminNameAndPassword(username, password);
            if (admin != null)
            {
                // Admin login successful
                MessageBox.Show($"Welcome Admin: {admin.AdminName}", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AdminWelcome adminWelcome = new AdminWelcome(admin.AdminId);
                this.Hide();
                adminWelcome.ShowDialog();
                this.Hide();
                return;
            }

            // If not admin, try to log in as Client (User)
            User user = _userRepository.GetUserByUsernameAndPassword(username, password);
            if (user != null)
            {
                // Client (User) login successful
                MessageBox.Show($"Welcome User: {user.UserName}", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 UserView userview = new UserView(user);
                 userview.Show();
                this.Hide();
                return;
            }

            MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Clear();
            txtUsername.Focus();
 
        }

        private void Loginform_Load_1(object sender, EventArgs e)
        {

        }
    }
}
