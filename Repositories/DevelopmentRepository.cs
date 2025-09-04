using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class DevelopmentRepository
    {
        public bool AddDevelopmentLink(int vendorId, int gameId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO develop (vendor_id, game_id) VALUES (@VendorId, @GameId)", connection);
                command.Parameters.AddWithValue("@VendorId", vendorId);
                command.Parameters.AddWithValue("@GameId", gameId);
                connection.Open();
                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch (SqlException ex) when (ex.Number == 2627)
                {
                    return false;
                }
            }
        }

        public bool RemoveDevelopmentLink(int vendorId, int gameId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM develop WHERE vendor_id = @VendorId AND game_id = @GameId", connection);
                command.Parameters.AddWithValue("@VendorId", vendorId);
                command.Parameters.AddWithValue("@GameId", gameId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<Game> GetGamesDevelopedByVendor(int vendorId)
        {
            var games = new List<Game>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"SELECT g.game_id, g.gameName, g.[date], g.stock
                                 FROM game g
                                 INNER JOIN develop d ON g.game_id = d.game_id
                                 WHERE d.vendor_id = @VendorId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VendorId", vendorId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new Game 
                        {
                            GameId = Convert.ToInt32(reader["game_id"]),
                            GameName = reader["gameName"].ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Stock = Convert.ToInt32(reader["stock"])
                        });
                    }
                }
            }
            return games;
        }

        public List<Vendor> GetDevelopersOfGame(int gameId)
        {
            var vendors = new List<Vendor>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"SELECT v.vendor_id, v.vendName, v.phone
                                 FROM vendor v
                                 INNER JOIN develop d ON v.vendor_id = d.vendor_id
                                 WHERE d.game_id = @GameId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GameId", gameId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vendors.Add(new Vendor 
                        {
                            VendorId = Convert.ToInt32(reader["vendor_id"]),
                            VendorName = reader["vendName"].ToString(),
                            PrimaryPhone = reader["phone"]?.ToString()
                        });
                    }
                }
            }
            return vendors;
        }
    }
}