namespace GameRentalSystem.Model.Entities.Reports
{
    public class SalesSummary
    {
        public int TotalRentals { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class PopularGame
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int RentalCount { get; set; }
    }

    public class RevenueByGame
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
// --- End of Report Item Entity Classes ---