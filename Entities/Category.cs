using System.Collections.Generic;

namespace GameRentalSystem.Model.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Navigation property (populated by repositories)
        public List<Game> Games { get; set; } = new List<Game>();
    }
}