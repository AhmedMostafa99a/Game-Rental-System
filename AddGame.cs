using System;
using System.Windows.Forms;
using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Repositories;

namespace GameRentalSystem
{
    public partial class AddGame : Form
    {
        private int adminId; 
        private Game game;  
        private Vendor vendor; 

        public AddGame(int adminIdWhoIsAdding) 
        {
            InitializeComponent();
            this.adminId = adminIdWhoIsAdding;
            game = new Game(); 
            vendor = new Vendor(); 
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Game Name
        {
            game.GameName = textBox2.Text.Trim();
        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Game Stock
        {
            if (int.TryParse(textBox4.Text.Trim(), out int stockAmount))
            {
                if (stockAmount >= 0)
                {
                    game.Stock = stockAmount;
                }
                else
                {
                    MessageBox.Show("Stock cannot be negative.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Clear(); // Or set to 0
                }
            }
            else if (!string.IsNullOrWhiteSpace(textBox4.Text)) // Only show error if not empty and not a number
            {
                MessageBox.Show("Invalid input for stock. Please enter a whole number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Clear();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) // Game Date
        {
            game.Date = dateTimePicker1.Value;
        }

        
        private void textBoxVendorName_TextChanged(object sender, EventArgs e)
        {
            vendor.VendorName = textBoxVendorName.Text.Trim();
        }

        private void textBoxVendorPhone_TextChanged(object sender, EventArgs e)
        {
            vendor.PrimaryPhone = textBoxVendorPhone.Text.Trim();
            // Basic validation for phone could be added here (e.g., length, numeric)
        }





        private void button1_Click(object sender, EventArgs e) // Add Game Button
        {
            
            game.GameName = textBox2.Text.Trim(); // Game Name
            if (int.TryParse(textBox4.Text.Trim(), out int stockAmount) && stockAmount >= 0)
            {
                game.Stock = stockAmount;
            }
            else if (!string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Invalid input for stock. Please enter a non-negative whole number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Clear();
                textBox4.Focus();
                return; // Stop processing if stock is invalid
            }
            game.Date = dateTimePicker1.Value;

            
            vendor.VendorName = textBoxVendorName.Text.Trim();
            vendor.PrimaryPhone = textBoxVendorPhone.Text.Trim();
            // --- End of explicit updates ---


            // Validate required fields
            if (string.IsNullOrWhiteSpace(game.GameName))
            {
                MessageBox.Show("Game Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text)) // Check raw text for stock presence
            {
                MessageBox.Show("Game Stock is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }
            // Now validate the vendor object's property
            if (string.IsNullOrWhiteSpace(vendor.VendorName))
            {
                MessageBox.Show("Vendor Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxVendorName.Focus();
                return;
            }
            // Phone could be optional, add validation if it's mandatory


            // --- Transactional Process for adding Game, Vendor, and Links ---
            GameRepository gameRepo = new GameRepository();
            VendorRepository vendorRepo = new VendorRepository();
            DevelopmentRepository devRepo = new DevelopmentRepository();
            AddGameLogRepository logRepo = new AddGameLogRepository();

            int newGameId = 0;
            int newVendorId = 0;

            if (!vendorRepo.AddVendor(this.vendor, out newVendorId)) // this.vendor now has the latest values
            {
                MessageBox.Show("Failed to add or find the vendor. Please check vendor details.", "Vendor Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!gameRepo.AddGame(this.game, out newGameId)) // this.game now has the latest values
            {
                MessageBox.Show("Failed to add the game. Please check game details.", "Game Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!devRepo.AddDevelopmentLink(newVendorId, newGameId))
            {
                MessageBox.Show("Failed to link game to vendor.", "Linking Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!logRepo.LogGameAddition(newGameId, this.adminId))
            {
                MessageBox.Show("Game added, but failed to log the addition.", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MessageBox.Show("Game, vendor, and links added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void AddGame_Load(object sender, EventArgs e)
        {
            // Set default date or other initializations if needed
            dateTimePicker1.Value = DateTime.Today;
            game.Date = DateTime.Today; // Initialize game object's date too
        }
    }
}