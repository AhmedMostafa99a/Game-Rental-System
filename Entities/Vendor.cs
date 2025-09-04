using GameRentalSystem.Model.Entities;
using System.Collections.Generic;

namespace GameRentalSystem.Model.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string PrimaryPhone { get; set; } // From vendor.phone

        // Navigation properties
        public List<VendorPhone> AdditionalPhones { get; set; } = new List<VendorPhone>();
        public List<Game> DevelopedGames { get; set; } = new List<Game>();
    }
}