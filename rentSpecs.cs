using System;
using System.Drawing; // For Point and Size
using System.Windows.Forms;
using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Repositories;

namespace GameRentalSystem
{
    public partial class rentSpecs : Form
    {
        private DateTimePicker rentDatePicker;
        private DateTimePicker returnDatePicker;
        private Button rentButton;
        private Button backToRentButton; // Consistent Naming
        private Label lblRentDate;      // Label for rent date picker
        private Label lblReturnDate;    // Label for return date picker
        private Label lblPriceInfo;     // Label to show calculated price info

        private int gameIdToRent;
        private User currentUser;

        public rentSpecs(int gameID, User user) // Changed parameter name for clarity
        {
            InitializeComponent();
            this.gameIdToRent = gameID;
            this.currentUser = user;

            this.Text = "Rental Specifications";
            this.Size = new Size(350, 250); // Adjust as needed

            // --- Create and Configure Controls ---

            // Rent Date Label and Picker
            lblRentDate = new Label();
            lblRentDate.Text = "Rent Date:";
            lblRentDate.Location = new Point(20, 20);
            lblRentDate.AutoSize = true;

            rentDatePicker = new DateTimePicker();
            rentDatePicker.Location = new Point(120, 20);
            rentDatePicker.Size = new Size(200, 25);
            rentDatePicker.MinDate = DateTime.Today; // Prevent selecting past dates

            // Return Date Label and Picker
            lblReturnDate = new Label();
            lblReturnDate.Text = "Return Date:";
            lblReturnDate.Location = new Point(20, 60);
            lblReturnDate.AutoSize = true;

            returnDatePicker = new DateTimePicker();
            returnDatePicker.Location = new Point(120, 60);
            returnDatePicker.Size = new Size(200, 25);
            returnDatePicker.MinDate = DateTime.Today; // Initially set, will be updated by rentDatePicker

            // Update returnDatePicker.MinDate when rentDatePicker changes
            rentDatePicker.ValueChanged += (s, ev) =>
            {
                returnDatePicker.MinDate = rentDatePicker.Value;
                if (returnDatePicker.Value < rentDatePicker.Value)
                {
                    returnDatePicker.Value = rentDatePicker.Value; // Ensure return date is not before rent date
                }
                UpdatePriceDisplay();
            };
            returnDatePicker.ValueChanged += (s, ev) => UpdatePriceDisplay();


            // Price Info Label
            lblPriceInfo = new Label();
            lblPriceInfo.Location = new Point(20, 100);
            lblPriceInfo.Size = new Size(300, 20);
            lblPriceInfo.Text = "Price: $0.00 (Calculated based on days)";


            // Rent Button
            rentButton = new Button();
            rentButton.Text = "Confirm Rent";
            rentButton.Location = new Point(120, 140);
            rentButton.Size = new Size(100, 30);
            rentButton.Click += RentButton_Click; // Use a separate method for clarity

            // Back to Rent Button
            backToRentButton = new Button(); // Changed name
            backToRentButton.Text = "Back";
            backToRentButton.Location = new Point(20, 140);
            backToRentButton.Size = new Size(80, 30);
            backToRentButton.Click += BackToRentButton_Click;

            // --- Add Controls to Form ---
            this.Controls.Add(lblRentDate);
            this.Controls.Add(rentDatePicker);
            this.Controls.Add(lblReturnDate);
            this.Controls.Add(returnDatePicker);
            this.Controls.Add(lblPriceInfo);
            this.Controls.Add(rentButton);
            this.Controls.Add(backToRentButton);

            // Initial price display
            UpdatePriceDisplay();
            // Ensure return date picker is valid initially
            if (returnDatePicker.Value < rentDatePicker.Value)
            {
                returnDatePicker.Value = rentDatePicker.Value;
            }
        }

        private void UpdatePriceDisplay()
        {
            if (returnDatePicker.Value.Date < rentDatePicker.Value.Date)
            {
                lblPriceInfo.Text = "Price: Invalid date selection";
                return;
            }

            // Calculate price (e.g., $1 per day, minimum 1 day if dates are different)
            TimeSpan rentalDuration = returnDatePicker.Value.Date - rentDatePicker.Value.Date;
            int days = (int)rentalDuration.TotalDays;

            decimal pricePerDay = 1.00m; // Example price
            decimal totalPrice = 0;

            if (days < 0) // Should not happen with MinDate logic but good to check
            {
                lblPriceInfo.Text = "Price: Return date before rent date!";
                return;
            }
            else if (days == 0) // Same day rental
            {
                totalPrice = pricePerDay; // Minimum 1 day charge
            }
            else
            {
                totalPrice = days * pricePerDay;
            }
            lblPriceInfo.Text = $"Price: ${totalPrice:F2}";
        }


        private void RentButton_Click(object sender, EventArgs e)
        {
            DateTime rentDate = rentDatePicker.Value.Date; // Use .Date to ignore time part for these comparisons
            DateTime returnDate = returnDatePicker.Value.Date;

            // Validate dates
            if (rentDate < DateTime.Today) // Redundant if MinDate is set, but good double check
            {
                MessageBox.Show("Rent date cannot be in the past.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (returnDate < rentDate)
            {
                MessageBox.Show("Return date cannot be before the rent date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate price
            TimeSpan rentalDuration = returnDate - rentDate;
            int days = (int)rentalDuration.TotalDays;
            decimal pricePerDay = 1.00m; // Define your price per day
            decimal finalPrice = 0;

            if (days == 0)
            { // Same day rental
                finalPrice = pricePerDay; // Minimum 1 day charge
            }
            else
            {
                finalPrice = days * pricePerDay;
            }


            // Confirm with user
            DialogResult confirmation = MessageBox.Show($"You are about to rent game ID: {gameIdToRent}\n" +
                                                       $"From: {rentDate:yyyy-MM-dd}\n" +
                                                       $"To: {returnDate:yyyy-MM-dd}\n" +
                                                       $"Total Price: ${finalPrice:F2}\n\nConfirm rental?",
                                                       "Confirm Rental", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmation == DialogResult.Yes)
            {
                RentRepository rentRepository = new RentRepository();
                Rent newRental = new Rent
                {
                    ClientId = currentUser.UserId, // Assuming UserId from User object is the ClientId
                    GameId = gameIdToRent,
                    RentDate = rentDate,
                    ReturnDate = returnDate, // Rent entity's ReturnDate should be Nullable<DateTime> if it can be null initially
                    RentCost = finalPrice
                };

                // Before adding rental, you might want to check game stock again
                GameRepository gameRepo = new GameRepository();
                Game gameToRent = gameRepo.GetGameById(gameIdToRent);
                if (gameToRent != null && gameToRent.Stock > 0)
                {
                    if (rentRepository.AddRental(newRental)) // AddRental should also handle decrementing stock
                    {
                        MessageBox.Show("Game rented successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Optionally update game stock in the Game object if AddRental doesn't do it,
                        // or better, AddRental method should handle stock decrement transactionally.
                        gameToRent.Stock--;
                        gameRepo.UpdateGame(gameToRent); // Make sure UpdateGame works

                        this.Close(); // Close this form
                        // You might want to refresh the UserView or RentaGameView if they are still open
                    }
                    else
                    {
                        MessageBox.Show("Failed to record the rental. Please try again.", "Rental Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, the game is out of stock or an error occurred fetching game details.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BackToRentButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            // To avoid creating multiple instances of RentaGameView,
            // you might need a way to get a reference to the existing one or manage open forms.
            // For simplicity, this creates a new one:
            RentaGameView rentaGame = new RentaGameView(currentUser);
            rentaGame.Show();
            this.Close(); // Close current form after showing the new one
        }

        private void rentSpecs_Load(object sender, EventArgs e)
        {
            // This is a good place to set initial values if not done in constructor
            // For example, if you want returnDate to be 1 day after rentDate by default:
            // returnDatePicker.Value = rentDatePicker.Value.AddDays(1);
            // Ensure it respects MinDate
            if (returnDatePicker.Value < rentDatePicker.Value)
            {
                returnDatePicker.Value = rentDatePicker.Value;
            }
            UpdatePriceDisplay();
        }
    }
}