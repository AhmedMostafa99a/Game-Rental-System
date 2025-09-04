using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class ClientRepository
    {
        public List<Client> GetAllClients()
        {
            var clients = new List<Client>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT 
                        c.client_id, c.clientName, c.pass AS ClientPass, c.email AS ClientEmail, c.address AS ClientAddress,
                        u.userName AS UserTableName, u.pass AS UserTablePass, u.email AS UserTableEmail, u.address AS UserTableAddress
                    FROM client c
                    INNER JOIN users u ON c.client_id = u.user_id";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(MapClientFromReader(reader));
                    }
                }
            }
            return clients;
        }

        public Client GetClientById(int clientId)
        {
            Client client = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT 
                        c.client_id, c.clientName, c.pass AS ClientPass, c.email AS ClientEmail, c.address AS ClientAddress,
                        u.userName AS UserTableName, u.pass AS UserTablePass, u.email AS UserTableEmail, u.address AS UserTableAddress
                    FROM client c
                    INNER JOIN users u ON c.client_id = u.user_id
                    WHERE c.client_id = @ClientId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientId", clientId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = MapClientFromReader(reader);
                    }
                }
            }
            return client;
        }

        public bool AddUser(User user, out int generatedUserId) // Modified to return the ID
        {
            generatedUserId = 0; // Default value
            using (var connection = DatabaseHelper.GetConnection())
            {

                string query = "INSERT INTO users (userName, pass, email, address) " +
                               "VALUES (@UserName, @Pass, @Email, @Address); " +
                               "SELECT SCOPE_IDENTITY();";
                // SCOPE_IDENTITY() is generally safer than @@IDENTITY

                var command = new SqlCommand(query, connection);
                // DO NOT ADD: command.Parameters.AddWithValue("@UserId", user.UserId);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Pass", user.Password); // TODO: HASH THIS PASSWORD
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Address", user.Address);

                connection.Open();
                try
                {
                    // ExecuteScalar is used because we expect a single value (the new ID) back
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        generatedUserId = Convert.ToInt32(result);
                        user.UserId = generatedUserId; // Update the user object with the new ID
                        return true;
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    // Handle potential errors, e.g., unique constraint violation for userName or email
                    // You can log ex.Message or throw a custom exception
                    Console.WriteLine("SQL Error in AddUser: " + ex.Message); // Basic logging
                    return false;
                }
            }
        }

        public bool UpdateClient(Client client)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string userQuery = "UPDATE users SET userName = @UserName, pass = @Pass, email = @Email, address = @Address WHERE user_id = @UserId";
                        var userCommand = new SqlCommand(userQuery, connection, transaction);
                        userCommand.Parameters.AddWithValue("@UserName", client.UserNameForUsersTable);
                        userCommand.Parameters.AddWithValue("@Pass", client.PasswordForUsersTable);
                        userCommand.Parameters.AddWithValue("@Email", client.EmailForUsersTable);
                        userCommand.Parameters.AddWithValue("@Address", client.AddressForUsersTable);
                        userCommand.Parameters.AddWithValue("@UserId", client.ClientId);
                        userCommand.ExecuteNonQuery();

                        string clientQuery = "UPDATE client SET clientName = @ClientName, pass = @Pass, email = @Email, address = @Address WHERE client_id = @ClientId";
                        var clientCommand = new SqlCommand(clientQuery, connection, transaction);
                        clientCommand.Parameters.AddWithValue("@ClientName", client.ClientName);
                        clientCommand.Parameters.AddWithValue("@Pass", client.Password);
                        clientCommand.Parameters.AddWithValue("@Email", client.Email);
                        clientCommand.Parameters.AddWithValue("@Address", client.Address);
                        clientCommand.Parameters.AddWithValue("@ClientId", client.ClientId);
                        clientCommand.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception) 
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool DeleteClient(int clientId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Order: Delete from 'client' first due to FK on client.client_id referencing users.user_id
                        var clientCommand = new SqlCommand("DELETE FROM client WHERE client_id = @ClientId", connection, transaction);
                        clientCommand.Parameters.AddWithValue("@ClientId", clientId);
                        int clientRowsAffected = clientCommand.ExecuteNonQuery();

                        var userCommand = new SqlCommand("DELETE FROM users WHERE user_id = @UserId", connection, transaction);
                        userCommand.Parameters.AddWithValue("@UserId", clientId);
                        int userRowsAffected = userCommand.ExecuteNonQuery();

                        if (clientRowsAffected > 0 || userRowsAffected > 0) // Successful if at least one was deleted (user might not exist if FK was broken)
                        {                                                // Or stricter: if (clientRowsAffected > 0 && userRowsAffected > 0)
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback(); // No records found or error
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

        public Client GetClientByUsernameAndPassword(string username, string password)
        {
            Client client = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT 
                        c.client_id, c.clientName, c.pass AS ClientPass, c.email AS ClientEmail, c.address AS ClientAddress,
                        u.userName AS UserTableName, u.pass AS UserTablePass, u.email AS UserTableEmail, u.address AS UserTableAddress
                    FROM client c
                    INNER JOIN users u ON c.client_id = u.user_id
                    WHERE u.userName = @Username AND c.pass = @Password";


                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password); 
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = MapClientFromReader(reader); 
                    }
                }
            }
            return client;
        }


        private Client MapClientFromReader(SqlDataReader reader)
        {
            return new Client
            {
                ClientId = Convert.ToInt32(reader["client_id"]),
                ClientName = reader["ClientName"].ToString(),
                Password = reader["ClientPass"].ToString(),
                Email = reader["ClientEmail"].ToString(),
                Address = reader["ClientAddress"].ToString(),
                UserNameForUsersTable = reader["UserTableName"].ToString(),
                PasswordForUsersTable = reader["UserTablePass"].ToString(),
                EmailForUsersTable = reader["UserTableEmail"].ToString(),
                AddressForUsersTable = reader["UserTableAddress"].ToString()
            };
        }
    }
}