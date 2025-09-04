using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class UserRepository
    {
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT user_id, userName, pass, email, address FROM users", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(MapUserFromReader(reader));
                    }
                }
            }
            return users;
        }

        public User GetUserById(int userId)
        {
            User user = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT user_id, userName, pass, email, address FROM users WHERE user_id = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapUserFromReader(reader);
                    }
                }
            }
            return user;
        }

        public bool AddUser(User user)
        {

            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO users (userName, pass, email, address) VALUES (@UserName, @Pass, @Email, @Address)", connection);
                // command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Pass", user.Password);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Address", user.Address);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateUser(User user)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("UPDATE users SET userName = @UserName, pass = @Pass, email = @Email, address = @Address WHERE user_id = @UserId", connection);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Pass", user.Password);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@UserId", user.UserId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM users WHERE user_id = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            User user = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT user_id, userName, pass, email, address FROM users WHERE userName = @UserName AND pass = @Pass", connection);
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@Pass", password);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapUserFromReader(reader);
                    }
                }
            }
            return user;
        }

        private User MapUserFromReader(SqlDataReader reader)
        {
            return new User
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                UserName = reader["userName"].ToString(),
                Password = reader["pass"].ToString(),
                Email = reader["email"].ToString(),
                Address = reader["address"].ToString()
            };
        }
    }
}