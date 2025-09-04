using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class GameRepository
    {
        public List<Game> GetAllGames()
        {
            var games = new List<Game>();
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    var command = new SqlCommand("SELECT game_id, gameName, [date], stock FROM game", connection);
                    connection.Open(); // Breakpoint here
                    using (var reader = command.ExecuteReader()) 
                    {
                        while (reader.Read()) // Breakpoint here
                        {
                            games.Add(MapGameFromReader(reader));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error in GetAllGames: {sqlEx.Message}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error in GetAllGames: {ex.Message}");
                
            }
            return games; 
        }

        public Game GetGameById(int gameId)
        {
            Game game = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT game_id, gameName, [date], stock FROM game WHERE game_id = @GameId", connection);
                command.Parameters.AddWithValue("@GameId", gameId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        game = MapGameFromReader(reader);
                    }
                }
            }

            return game;
        }

        public bool AddGame(Game game, out int generatedGameId) 
        {
            generatedGameId = 0;
            using (var connection = DatabaseHelper.GetConnection())
            {
                string query = "INSERT INTO game (gameName, [date], stock) " +
                               "VALUES (@GameName, @Date, @Stock); SELECT SCOPE_IDENTITY();";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GameName", game.GameName);
                command.Parameters.AddWithValue("@Date", game.Date);
                command.Parameters.AddWithValue("@Stock", game.Stock);
                connection.Open();
                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        generatedGameId = Convert.ToInt32(result);
                        game.GameId = generatedGameId; // Update game object with new ID
                        return true;
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error adding game: " + ex.Message); // Log error
                    return false;
                }
            }
        }

        public bool UpdateGame(Game game)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("UPDATE game SET gameName = @GameName, [date] = @Date, stock = @Stock WHERE game_id = @GameId", connection);
                command.Parameters.AddWithValue("@GameName", game.GameName);
                command.Parameters.AddWithValue("@Date", game.Date);
                command.Parameters.AddWithValue("@Stock", game.Stock);
                command.Parameters.AddWithValue("@GameId", game.GameId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteGame(int gameId)
        {

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Example: Delete from junction tables first
                        var cmdGameCategory = new SqlCommand("DELETE FROM game_category WHERE game_id = @GameId", connection, transaction);
                        cmdGameCategory.Parameters.AddWithValue("@GameId", gameId);
                        cmdGameCategory.ExecuteNonQuery();

                        var cmdRent = new SqlCommand("DELETE FROM rent WHERE game_id = @GameId", connection, transaction);
                        cmdRent.Parameters.AddWithValue("@GameId", gameId);
                        cmdRent.ExecuteNonQuery();

                        var cmdAddGame = new SqlCommand("DELETE FROM add_game WHERE game_id = @GameId", connection, transaction);
                        cmdAddGame.Parameters.AddWithValue("@GameId", gameId);
                        cmdAddGame.ExecuteNonQuery();

                        var cmdDevelop = new SqlCommand("DELETE FROM develop WHERE game_id = @GameId", connection, transaction);
                        cmdDevelop.Parameters.AddWithValue("@GameId", gameId);
                        cmdDevelop.ExecuteNonQuery();

                        // Then delete from the game table
                        var command = new SqlCommand("DELETE FROM game WHERE game_id = @GameId", connection, transaction);
                        command.Parameters.AddWithValue("@GameId", gameId);
                        int rowsAffected = command.ExecuteNonQuery();

                        transaction.Commit();
                        return rowsAffected > 0;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private Game MapGameFromReader(SqlDataReader reader)
        {
            return new Game
            {
                GameId = Convert.ToInt32(reader["game_id"]),
                GameName = reader["gameName"].ToString(),
                Date = Convert.ToDateTime(reader["date"]),
                Stock = Convert.ToInt32(reader["stock"])
            };
        }
    }
}