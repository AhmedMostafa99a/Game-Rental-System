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


namespace GameRentalSystem
{
    public partial class AdminWelcome : Form
    {
    private int num;
        public AdminWelcome(int n)
        {
            InitializeComponent();
            num = n;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddGame form2 = new AddGame(this.num);
            form2.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DelGame form4 = new DelGame();
            form4.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateGame form3 = new UpdateGame();
            form3.ShowDialog(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.ShowDialog(this);

        }
    }
}
