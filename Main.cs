using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameRentalSystem
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("For more information, visit: https://github.com/Abdullahali77/Games-Rental-System-", "GitHub Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Signup form2 = new Signup();
            form2.ShowDialog(this);
        }

        private void log_in_Click(object sender, EventArgs e)
        {
            Loginform form1 = new Loginform(false);
            form1.ShowDialog(this);
        }
    }
}