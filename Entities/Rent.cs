using System;

namespace GameRentalSystem.Model.Entities
{
    public class Rent
    {
        public Rent(int clientId, int gameId, DateTime rentDate, DateTime? returnDate, decimal rentCost)
        {
            ClientId = clientId;
            GameId = gameId;
            RentDate = rentDate;
            ReturnDate = returnDate;
            RentCost = rentCost;
        }

        public Rent()
        {
        }

        public int ClientId { get; set; }
        public int GameId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Nullable if not returned
        public decimal RentCost { get; set; }

        // Optional: Navigation properties
        // public Client Client { get; set; }
        // public Game Game { get; set; }
    }
}