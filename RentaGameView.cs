using System;
using System.Collections.Generic;
using System.Drawing; // For Point and Size
using System.Linq;
using System.Windows.Forms;
using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Repositories;

namespace GameRentalSystem
{
    public partial class RentaGameView : Form
    {
        private ComboBox comboBoxGames; // Renamed for clarity
        private Button buttonRent;
        private Button buttonReturnToUserView;
        private List<Game> allGamesList; // Store the original full list for filtering
        private Timer searchTimer;
        private User currentUser; // Store the user for convenience

        public RentaGameView(User user)
        {
            InitializeComponent();
            this.currentUser = user; // Store the user

            this.Text = "Rent a Game"; // Set a title for the form
            this.Size = new Size(400, 200); // Adjust form size as needed

            // Create and add controls
            createComboBoxOfAvailableGames();
            createRentButton(); // Removed user parameter as we store it in currentUser
            createReturnToUserViewButton(); // Removed user parameter

            // Add controls to the form
            if (comboBoxGames != null) this.Controls.Add(comboBoxGames);
            if (buttonRent != null) this.Controls.Add(buttonRent);
            if (buttonReturnToUserView != null) this.Controls.Add(buttonReturnToUserView);
        }

        public void createComboBoxOfAvailableGames()
        {
            GameRepository gameRepository = new GameRepository();
            allGamesList = gameRepository.GetAllGames(); // Fetch all games once

            comboBoxGames = new ComboBox();
            comboBoxGames.Location = new Point(20, 20);
            comboBoxGames.Size = new Size(350, 25);
            comboBoxGames.DataSource = allGamesList; // Bind to the full list initially

            // Configure Display and Value members
            comboBoxGames.DisplayMember = "GameName"; // Show the game's name
            comboBoxGames.ValueMember = "GameId";   // Use GameId as the underlying value

            // To show stock, you might need to customize how GameName is displayed
            // e.g., by overriding ToString() in your Game class or creating a wrapper class.
            // For simplicity, we'll just display GameName.
            // If you want to display "GameName (Stock: X)", you'd do:
            // comboBoxGames.FormatString = "GameName (Stock: {0})"; // This doesn't work directly
            // You would need a property in Game like: public string DisplayNameWithStock => $"{GameName} (Stock: {Stock})";
            // And then set comboBoxGames.DisplayMember = "DisplayNameWithStock";

            comboBoxGames.DropDownStyle = ComboBoxStyle.DropDown; // Allows typing for search
            // comboBoxGames.AutoCompleteMode = AutoCompleteMode.SuggestAppend; // Optional: for better auto-completion
            // comboBoxGames.AutoCompleteSource = AutoCompleteSource.ListItems;   // Optional

            searchTimer = new Timer();
            searchTimer.Interval = 300; // milliseconds
            searchTimer.Tick += SearchTimer_Tick;

            comboBoxGames.TextChanged += ComboBox_TextChanged;
            // Consider also comboBoxGames.KeyDown for more immediate filtering on Enter, etc.
        }


        public void createRentButton()
        {
            buttonRent = new Button();
            buttonRent.Text = "Rent Selected Game";
            buttonRent.Location = new Point(20, 60);
            buttonRent.Size = new Size(150, 30);
            buttonRent.Click += (object sender, EventArgs e) =>
            {
                if (comboBoxGames.SelectedItem is Game selectedGame) // Type check and cast
                {
                    // It's good practice to check if a game is actually selected
                    if (selectedGame.Stock > 0)
                    {
                        this.Hide();
                        // Assuming rentSpecs form exists and takes gameId and User
                        rentSpecs rentSpecsForm = new rentSpecs(selectedGame.GameId, currentUser);
                        rentSpecsForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("This game is currently out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a game from the list.", "No Game Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
        }

        public void createReturnToUserViewButton()
        {
            buttonReturnToUserView = new Button();
            buttonReturnToUserView.Text = "Back to Games List";
            buttonReturnToUserView.Location = new Point(200, 60); // Position it next to the rent button
            buttonReturnToUserView.Size = new Size(150, 30);
            buttonReturnToUserView.Click += (object sender, EventArgs e) =>
            {
                this.Hide();
                UserView userViewForm = new UserView(currentUser); // Create a new UserView or find existing
                userViewForm.Show();
                // If you want to ensure only one UserView is open, you'll need more complex form management.
            };
        }

        public void ComboBox_TextChanged(object sender, EventArgs e)
        {
            // Restart the timer on text change to delay search
            searchTimer.Stop();
            searchTimer.Start();
        }

        public void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop(); // Stop the timer

            string searchText = comboBoxGames.Text; // No Trim() here, to allow searching with leading/trailing spaces if desired during typing

            // Filter games from the original full list
            // This ensures that if the user clears the search text, all games are shown again.
            var filteredGames = allGamesList
                .Where(game => game.GameName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            // Preserve current text and selection if possible
            string currentComboBoxText = comboBoxGames.Text;
            int selectionStart = comboBoxGames.SelectionStart;
            int selectionLength = comboBoxGames.SelectionLength;

            // Update ComboBox data source
            // Important: Check if the data source is actually changing to avoid unnecessary refreshes
            // or potential issues if the current selected item is removed by the filter.
            comboBoxGames.DataSource = null; // Temporarily unbind to allow text to remain
            comboBoxGames.DataSource = filteredGames;
            comboBoxGames.DisplayMember = "GameName"; // Re-assign after changing DataSource
            comboBoxGames.ValueMember = "GameId";   // Re-assign

            // Try to restore text, this helps keep what the user typed.
            // This part can be tricky because changing DataSource might clear the text.
            if (comboBoxGames.Items.Count > 0 || !string.IsNullOrEmpty(currentComboBoxText))
            {
                // If the text the user typed is still present in the filtered list as an item,
                // it might get selected automatically. If not, we restore the text.
                var itemMatchingText = filteredGames.FirstOrDefault(g => g.GameName.Equals(currentComboBoxText, StringComparison.OrdinalIgnoreCase));
                if (itemMatchingText == null && !string.IsNullOrEmpty(currentComboBoxText))
                {
                    comboBoxGames.Text = currentComboBoxText; // Restore typed text
                }

                // Restore cursor position
                if (selectionStart <= comboBoxGames.Text.Length)
                {
                    comboBoxGames.SelectionStart = selectionStart;
                    comboBoxGames.SelectionLength = selectionLength;
                }
                else
                {
                    comboBoxGames.SelectionStart = comboBoxGames.Text.Length; // Move to end if original position is invalid
                }
            }


            if (!string.IsNullOrEmpty(searchText))
            {
                comboBoxGames.DroppedDown = true; // Keep dropdown open while searching
            }
            else
            {
                comboBoxGames.DroppedDown = false;
            }

            // Ensure focus remains on the ComboBox for continuous typing
            comboBoxGames.Focus();
        }

  

        private void RentaGameView_Load(object sender, EventArgs e)
        {
      
        }


        private void RentaGameView_FormClosed(object sender, FormClosedEventArgs e)
        {
      
        }
    }
}