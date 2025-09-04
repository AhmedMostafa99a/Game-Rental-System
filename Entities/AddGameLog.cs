namespace GameRentalSystem.Model.Entities
{
    public class AddGameLog 
    {
        public int GameId { get; set; }
        public int AdminId { get; set; }

        // Optional: Navigation properties
        // public Game Game { get; set; }
        // public Admin Admin { get; set; }
    }
}