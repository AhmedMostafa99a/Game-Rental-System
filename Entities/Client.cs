namespace GameRentalSystem.Model.Entities
{
    public class Client
    {
        // From 'client' table
        public int ClientId { get; set; } // This will be the same as User.UserId
        public string ClientName { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; }    
        public string Address { get; set; }  

        // These properties are for populating the linked 'users' table record.
        // The 'UserId' for the 'users' table will be Client.ClientId.
        public string UserNameForUsersTable { get; set; }
        public string PasswordForUsersTable { get; set; } // Password for general user context in 'users' table
        public string EmailForUsersTable { get; set; }    // Email for general user context in 'users' table
        public string AddressForUsersTable { get; set; }  // Address for general user context in 'users' table
    }
}