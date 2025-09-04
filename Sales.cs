using System;
using System.Collections.Generic;
using System.Drawing; // For Point and Size
using System.Linq;
using System.Windows.Forms;
using GameRentalSystem.Model.Entities.Reports; // Your report item classes
using GameRentalSystem.Model.Repositories;    // Your RentRepository

namespace GameRentalSystem
{
    public partial class Sales : Form // partial keyword is crucial for designer file
    {
        private RentRepository _rentRepository;
        private DataGridView dgvReportResults;
        private Button btnOverallSummary;
        private Button btnMostRented;
        private Button btnTopRevenue;
        private Button btnClose;

        private Label lblTopN;
        private NumericUpDown nudTopN;

        public Sales()
        {
            // InitializeComponent() is called here, from the designer file.
            // It sets up the basic form properties (ClientSize, Text, etc.).
            InitializeComponent();

            _rentRepository = new RentRepository();
            InitializeCustomComponents(); // Our method to add dynamic UI elements
        }

        private void InitializeCustomComponents()
        {
            // You can override/adjust properties set in InitializeComponent() here if needed.
            // For example, if you want a different title than "Sales" from designer:
            this.Text = "Sales & Rental Reports";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(800, 600); // Adjust default size for better layout
            this.Padding = new Padding(10); // Add some padding around the edges

            // --- Top N Selection Controls ---
            lblTopN = new Label
            {
                Text = "Top N:",
                Location = new Point(10, 15),
                AutoSize = true
            };
            this.Controls.Add(lblTopN);

            nudTopN = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 50,
                Value = 5, // Default Top N value
                Location = new Point(lblTopN.Right + 5, 12),
                Size = new Size(60, 25)
            };
            this.Controls.Add(nudTopN);


            // --- Report Buttons ---
            btnOverallSummary = new Button
            {
                Text = "Overall Sales Summary",
                Location = new Point(10, nudTopN.Bottom + 15), // Position below Top N controls
                Size = new Size(180, 30)
            };
            btnOverallSummary.Click += BtnOverallSummary_Click;
            this.Controls.Add(btnOverallSummary);

            btnMostRented = new Button
            {
                Text = "Most Rented Games",
                Location = new Point(btnOverallSummary.Right + 10, btnOverallSummary.Top),
                Size = new Size(180, 30)
            };
            btnMostRented.Click += BtnMostRented_Click;
            this.Controls.Add(btnMostRented);

            btnTopRevenue = new Button
            {
                Text = "Top Revenue Games",
                Location = new Point(btnMostRented.Right + 10, btnOverallSummary.Top),
                Size = new Size(180, 30)
            };
            btnTopRevenue.Click += BtnTopRevenue_Click;
            this.Controls.Add(btnTopRevenue);

            // --- DataGridView for List Reports ---
            dgvReportResults = new DataGridView
            {
                Location = new Point(10, btnOverallSummary.Bottom + 15), // Position below buttons
                Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - btnOverallSummary.Bottom - 55), // Adjusted height
                AllowUserToAddRows = false, // Prevents an empty row at the bottom
                ReadOnly = true,           // Makes the grid read-only
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, // Columns fill the width
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right // Makes it resize with form
            };
            this.Controls.Add(dgvReportResults);

            // --- Close Button ---
            btnClose = new Button
            {
                Text = "Close",
                Location = new Point(this.ClientSize.Width - 110, nudTopN.Bottom + 15), // Align with other buttons
                Size = new Size(100, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Right // Anchor to top right
            };
            btnClose.Click += (sender, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        // --- Event Handlers for Buttons ---
        private void BtnOverallSummary_Click(object sender, EventArgs e)
        {
            try
            {
                SalesSummary summary = _rentRepository.GetOverallSalesSummary();
                MessageBox.Show(this, // Pass 'this' to make MessageBox modal to this form
                                $"Overall Sales Summary:\n\n" +
                                $"Total Games Rented: {summary.TotalRentals}\n" +
                                $"Total Revenue Generated: ${summary.TotalRevenue:N2}", // N2 for currency format
                                "Sales Summary",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error fetching sales summary: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMostRented_Click(object sender, EventArgs e)
        {
            try
            {
                int topN = (int)nudTopN.Value;
                List<PopularGame> popularGames = _rentRepository.GetMostRentedGames(topN);

                dgvReportResults.DataSource = null; // Clear previous data
                if (popularGames != null && popularGames.Any())
                {
                    dgvReportResults.DataSource = popularGames;
                    // Optional: Customize column headers for better readability
                    dgvReportResults.Columns["GameId"].HeaderText = "Game ID";
                    dgvReportResults.Columns["GameName"].HeaderText = "Game Name";
                    dgvReportResults.Columns["RentalCount"].HeaderText = "Times Rented";
                }
                else
                {
                    MessageBox.Show(this, "No data found for most rented games matching criteria.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvReportResults.DataSource = null; // Ensure grid is empty
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error fetching popular games: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTopRevenue_Click(object sender, EventArgs e)
        {
            try
            {
                int topN = (int)nudTopN.Value;
                List<RevenueByGame> revenueGames = _rentRepository.GetTopRevenueGames(topN);

                dgvReportResults.DataSource = null; // Clear previous data
                if (revenueGames != null && revenueGames.Any())
                {
                    dgvReportResults.DataSource = revenueGames;
                    // Customize column headers and formatting
                    dgvReportResults.Columns["GameId"].HeaderText = "Game ID";
                    dgvReportResults.Columns["GameName"].HeaderText = "Game Name";
                    dgvReportResults.Columns["TotalRevenue"].HeaderText = "Total Revenue";
                    dgvReportResults.Columns["TotalRevenue"].DefaultCellStyle.Format = "c2"; // Currency format
                }
                else
                {
                    MessageBox.Show(this, "No data found for top revenue games matching criteria.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvReportResults.DataSource = null; // Ensure grid is empty
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error fetching top revenue games: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // This is the default Load event handler wired in your Sales.Designer.cs
        private void Sales_Load(object sender, EventArgs e)
        {

        }
    }
}