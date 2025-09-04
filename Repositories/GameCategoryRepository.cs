using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class GameCategoryRepository
    {
        public bool AddGameToCategory(int gameId, int categoryId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO game_category (game_id, category_id) VALUES (@GameId, @CategoryId)", connection);
                command.Parameters.AddWithValue("@GameId", gameId);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                connection.Open();
                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch (SqlException ex) when (ex.Number == 2627) // Primary key violation
                {
                    // Link already exists, or one of the IDs is invalid causing FK violation
                    return false;
                }
            }
        }

        public bool RemoveGameFromCategory(int gameId, int categoryId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM game_category WHERE game_id = @GameId AND category_id = @CategoryId", connection);
                command.Parameters.AddWithValue("@GameId", gameId);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<Category> GetCategoriesForGame(int gameId)
        {
            var categories = new List<Category>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"SELECT c.category_id, c.category_name 
                                 FROM category c
                                 INNER JOIN game_category gc ON c.category_id = gc.category_id
                                 WHERE gc.game_id = @GameId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GameId", gameId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryId = Convert.ToInt32(reader["category_id"]),
                            CategoryName = reader["category_name"].ToString()
                        });
                    }
                }
            }
            return categories;
        }

        public List<Game> GetGamesForCategory(int categoryId)
        {
            var games = new List<Game>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = @"SELECT g.game_id, g.gameName, g.[date], g.stock
                                 FROM game g
                                 INNER JOIN game_category gc ON g.game_id = gc.game_id
                                 WHERE gc.category_id = @CategoryId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
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
    }
}