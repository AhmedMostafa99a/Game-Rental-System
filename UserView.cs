using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Repositories;

namespace GameRentalSystem
{
    public partial class UserView : Form
    {
        public UserView(User user)
        {
            InitializeComponent();
            createDataTable();
            createRentedGamesButton(user);
            createRentAGameViewButton(user);
            //we need a button to go to a form where the user can actually rent games
        }

        // Create a DataTable and add columns
        private void createDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Stock", typeof(int));
            dataTable.Columns.Add("Date", typeof(DateTime));

            //create the table of games 
            GameRepository repository = new GameRepository();
            List<Game> games = repository.GetAllGames();
            foreach (Game game in games)
            {
                dataTable.Rows.Add(game.GameId, game.GameName, game.Stock, game.Date);
            }

            // Create a DataGridView to display the data
            DataGridView dataGridView = new DataGridView();
            dataGridView.DataSource = dataTable;
            dataGridView.Size = new Size(500, 500);
            dataGridView.Location = new Point(50, 50);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                    int gameId = Convert.ToInt32(row.Cells["ID"].Value);
                    Game selectedGame = repository.GetGameById(gameId);
                    // Show the game details in a new form or a message box
                    MessageBox.Show($"Game ID: {selectedGame.GameId}\nName: {selectedGame.GameName}\nStock: {selectedGame.Stock}\nDate: {selectedGame.Date}");
                }
            };
             this.Controls.Add(dataGridView);

        }

        //create a button to go to the user's rented games
        private void createRentedGamesButton(User user)
        {
            Button rentedGamesButton = new Button();
            rentedGamesButton.Text = "Rented Games";
            rentedGamesButton.Size = new Size(100, 50);
            rentedGamesButton.Click += (object sender, EventArgs e) =>
            {
                this.Hide();
                RentalsView form6 = new RentalsView(user);
                form6.Show();
            };
            this.Controls.Add(rentedGamesButton);
        }

        private void createRentAGameViewButton(User user)
        {
            Button rentAGameViewButton = new Button();
            rentAGameViewButton.Text = "Rent A Game View";
            rentAGameViewButton.Size = new Size(100, 50);
            rentAGameViewButton.Location = new Point(600,30);
            rentAGameViewButton.Click += (object sender, EventArgs e) =>
            {
                this.Hide();
                RentaGameView rentAGameView = new RentaGameView(user);
                rentAGameView.Show();
            };
            this.Controls.Add(rentAGameViewButton);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void UserView_Load(object sender, EventArgs e)
        {

        }
    }
}