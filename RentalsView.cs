using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Repositories;

namespace GameRentalSystem
{
    public partial class RentalsView : Form
    {
        private readonly User user;
        private readonly RentRepository rentRepo;
        private DataGridView rentalsGrid;
        private Button returnButton;
        private Button backButton;

        public RentalsView(User user)
        {
            InitializeComponent();
            this.user = user;
            this.rentRepo = new RentRepository();

            this.Text = $"Rentals for {user.UserName}";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeUI();
            LoadUserRentals();
        }

        private void InitializeUI()
        {
            rentalsGrid = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(740, 330),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(rentalsGrid);

            returnButton = new Button
            {
                Text = "Return Selected Rental",
                Location = new Point(20, 370),
                Size = new Size(180, 40)
            };
            returnButton.Click += ReturnSelectedRental;
            this.Controls.Add(returnButton);

            backButton = new Button
            {
                Text = "Back to Dashboard",
                Location = new Point(220, 370),
                Size = new Size(180, 40)
            };
            backButton.Click += (sender, e) =>
            {
                this.Hide();
                new UserView(user).Show();
            };
            this.Controls.Add(backButton);
        }

        private void LoadUserRentals()
        {
            var rentals = rentRepo.GetRentalsByClientId(user.UserId);
            var table = new DataTable();

            table.Columns.Add("GameID", typeof(int));
            table.Columns.Add("RentDate", typeof(DateTime));
            table.Columns.Add("ReturnDate", typeof(DateTime));
            table.Columns.Add("RentCost", typeof(int));

            foreach (var rental in rentals)
            {
                table.Rows.Add(rental.GameId, rental.RentDate, rental.ReturnDate, rental.RentCost);
            }

            rentalsGrid.DataSource = table;
        }

        private void ReturnSelectedRental(object sender, EventArgs e)
        {
            if (rentalsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a rental to return.");
                return;
            }

            DataGridViewRow selectedRow = rentalsGrid.SelectedRows[0];

            int gameId = Convert.ToInt32(selectedRow.Cells["GameID"].Value);
            DateTime rentDate = Convert.ToDateTime(selectedRow.Cells["RentDate"].Value);

            bool deleted = rentRepo.DeleteRental(user.UserId, gameId, rentDate);

            if (deleted)
            {
                MessageBox.Show("Rental returned successfully.");
                LoadUserRentals();
            }
            else
            {
                MessageBox.Show("Failed to return the selected rental.");
            }
        }

        private void RentalsView_Load(object sender, EventArgs e)
        {
            // Not used
        }
    }
}
