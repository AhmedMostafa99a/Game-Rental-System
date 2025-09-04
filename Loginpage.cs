using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GameRentalSystem
{
    public partial class Form1 : Form
    {
        private static string _connectionString = "Server=Abdullah;Database=Game_Rent;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void CenterControls()
        {
            // Group controls vertically (assumes form size is fixed)
            int spacing = 10;
            int totalHeight = txtUsername.Height + txtPassword.Height + chkIsAdmin.Height + btnLogin.Height + (spacing * 3);
            int startY = (this.ClientSize.Height - totalHeight) / 2;

            // Center horizontally and arrange vertically
            int centerX = (this.ClientSize.Width - txtUsername.Width) / 2;

            txtUsername.Location = new System.Drawing.Point(centerX, startY);
            txtPassword.Location = new System.Drawing.Point(centerX, txtUsername.Bottom + spacing);
            chkIsAdmin.Location = new System.Drawing.Point(centerX, txtPassword.Bottom + spacing);
            btnLogin.Location = new System.Drawing.Point(centerX, chkIsAdmin.Bottom + spacing);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = chkIsAdmin.Checked ? "Admin" : "User";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password AND Role = @role";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@role", role);

                        int result = (int)cmd.ExecuteScalar();

                        if (result > 0)
                        {
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // TODO: Navigate to main application
                        }
                        else
                        {
                            MessageBox.Show("Invalid username, password, or role.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
