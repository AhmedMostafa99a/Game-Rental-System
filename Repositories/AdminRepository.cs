using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class AdminRepository
    {
        public List<Admin> GetAllAdmins()
        {
            var admins = new List<Admin>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT 
                        a.admin_id, a.adminName, a.pass AS AdminPass, a.email AS AdminEmail,
                        u.userName AS UserTableName, u.pass AS UserTablePass, u.email AS UserTableEmail, u.address AS UserTableAddress
                    FROM admin a
                    INNER JOIN users u ON a.admin_id = u.user_id";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admins.Add(MapAdminFromReader(reader));
                    }
                }
            }
            return admins;
        }

        public Admin GetAdminById(int adminId)
        {
            Admin admin = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT 
                        a.admin_id, a.adminName, a.pass AS AdminPass, a.email AS AdminEmail,
                        u.userName AS UserTableName, u.pass AS UserTablePass, u.email AS UserTableEmail, u.address AS UserTableAddress
                    FROM admin a
                    INNER JOIN users u ON a.admin_id = u.user_id
                    WHERE a.admin_id = @AdminId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AdminId", adminId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        admin = MapAdminFromReader(reader);
                    }
                }
            }
            return admin;
        }

        public bool AddAdmin(Admin admin)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        string adminQuery = "INSERT INTO admin (adminName, pass, email) VALUES (@AdminName, @Pass, @Email)";
                        var adminCommand = new SqlCommand(adminQuery, connection, transaction);
                        adminCommand.Parameters.AddWithValue("@AdminName", admin.AdminName);
                        adminCommand.Parameters.AddWithValue("@Pass", admin.Password);
                        adminCommand.Parameters.AddWithValue("@Email", admin.Email);
                        adminCommand.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex) 
                    {
                        // log the exception
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool UpdateAdmin(Admin admin)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        string adminQuery = "UPDATE admin SET adminName = @AdminName, pass = @Pass, email = @Email WHERE admin_id = @AdminId";
                        var adminCommand = new SqlCommand(adminQuery, connection, transaction);
                        adminCommand.Parameters.AddWithValue("@AdminName", admin.AdminName);
                        adminCommand.Parameters.AddWithValue("@Pass", admin.Password);
                        adminCommand.Parameters.AddWithValue("@Email", admin.Email);
                        adminCommand.Parameters.AddWithValue("@AdminId", admin.AdminId);
                        adminCommand.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception) // Consider logging the exception
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool DeleteAdmin(int adminId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var adminCommand = new SqlCommand("DELETE FROM admin WHERE admin_id = @AdminId", connection, transaction);
                        adminCommand.Parameters.AddWithValue("@AdminId", adminId);
                        int adminRowsAffected = adminCommand.ExecuteNonQuery();

                        if (adminRowsAffected > 0)
                        {
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch (Exception) // Consider logging the exception
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public Admin GetAdminByAdminNameAndPassword(string adminNameInput, string passwordInput)
        {
            Admin admin = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                // Query ONLY the 'admin' table
                string query = @"
            SELECT 
                admin_id, adminName, pass, email
            FROM admin
            WHERE adminName = @AdminName AND pass = @Password;
        ";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AdminName", adminNameInput);
                command.Parameters.AddWithValue("@Password", passwordInput); 
                                                                            
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Admin found and password matches within the admin table
                        admin = new Admin
                        {
                            AdminId = Convert.ToInt32(reader["admin_id"]),
                            AdminName = reader["adminName"].ToString(),
                            Password = reader["pass"].ToString(), // The password from admin.pass
                            Email = reader["email"] != DBNull.Value ? reader["email"].ToString() : null,
                        };
                    }
                }
            }
            return admin;
        }

        private Admin MapAdminFromReader(SqlDataReader reader)
        {
            return new Admin
            {
                AdminId = Convert.ToInt32(reader["admin_id"]),
                AdminName = reader["AdminName"].ToString(),
                Password = reader["AdminPass"].ToString(),
                Email = reader["AdminEmail"].ToString(),
                UserNameForUsersTable = reader["UserTableName"].ToString(),
                PasswordForUsersTable = reader["UserTablePass"].ToString(),
                EmailForUsersTable = reader["UserTableEmail"].ToString(),
                AddressForUsersTable = reader["UserTableAddress"].ToString()
            };
        }
    }
}