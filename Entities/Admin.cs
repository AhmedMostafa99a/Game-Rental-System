namespace GameRentalSystem.Model.Entities
{
    public class Admin
    {
        // From 'admin' table
        public int AdminId { get; set; } // This will be the same as User.UserId
        public string AdminName { get; set; }
        public string Password { get; set; } // Password for admin context
        public string Email { get; set; }    // Email for admin context

        // These properties are for populating the linked 'users' table record.
        // The 'UserId' for the 'users' table will be Admin.AdminId.
        public string UserNameForUsersTable { get; set; }
        public string PasswordForUsersTable { get; set; }
        public string EmailForUsersTable { get; set; }
        public string AddressForUsersTable { get; set; } // 'users' table has address, 'admin' table does not.
    }
}