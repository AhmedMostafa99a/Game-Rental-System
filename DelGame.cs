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
    public partial class DelGame : Form
    {
        public DelGame()
        {
            InitializeComponent();
        }

        int id;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int number))
            {
                id = number;
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a number.");
                textBox1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddGameLogRepository list = new AddGameLogRepository();
            List<AddGameLog> gamelist = list.GetLogsForGame(id);
            if (gamelist.Count > 0)
            {
                if (list.Del(id))
                {
                    MessageBox.Show("Game Deleted Successfully");
                }
                else
                {
                    MessageBox.Show("Game Delete Failed");
                }
            }
            else
            {
                MessageBox.Show("Sorry, this game doesn't exist");
            }
        }

        private void DelGame_Load(object sender, EventArgs e)
        {

        }
    }
}
