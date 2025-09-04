namespace GameRentalSystem.Model.Entities
{
    public class Development 
    {
        public int VendorId { get; set; }
        public int GameId { get; set; }

        // Optional: Navigation properties
        // public Vendor Vendor { get; set; }
        // public Game Game { get; set; }
    }
}