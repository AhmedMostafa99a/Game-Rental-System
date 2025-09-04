using GameRentalSystem.Model.Entities;
using System;
using System.Collections.Generic;


namespace GameRentalSystem.Model.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public DateTime Date { get; set; }
        public int Stock { get; set; }

        // Navigation properties (populated by repositories)
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Vendor> Developers { get; set; } = new List<Vendor>();
    }
}