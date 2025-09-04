namespace GameRentalSystem.Model.Entities
{
    public class GameCategory
    {
        public int GameId { get; set; }
        public int CategoryId { get; set; }

        // Optional: Navigation properties to the actual Game and Category objects
        // public Game Game { get; set; }
        // public Category Category { get; set; }
    }
}