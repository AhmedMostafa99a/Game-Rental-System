using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class AddGameLogRepository
    {
        //public bool LogGameAddition(int gameId, int adminId)
        //{
        //    using (var connection = DatabaseHelper.GetConnection())
        //    {
        //        var command = new SqlCommand("INSERT INTO add_game (game_id, admin_id) VALUES (@GameId, @AdminId)", connection);
        //        command.Parameters.AddWithValue("@GameId", gameId);
        //        command.Parameters.AddWithValue("@AdminId", adminId);
        //        connection.Open();
        //        try
        //        {
        //            return command.ExecuteNonQuery() > 0;
        //        }
        //        catch (SqlException ex) when (ex.Number == 2627)
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool LogGameAddition(int gameId, int adminId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO add_game (game_id, admin_id) VALUES (@GameId, @AdminId)", connection);
                command.Parameters.AddWithValue("@GameId", gameId);
                command.Parameters.AddWithValue("@AdminId", adminId);
                connection.Open();
                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error logging game addition: " + ex.Message); // Log error
                    // ex.Number == 2627 for PK violation if (gameId, adminId) must be unique and already exists
                    return false;
                }
            }
        }

        public List<AddGameLog> GetLogsForGame(int gameId)
        {
            var logs = new List<AddGameLog>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT game_id FROM add_game WHERE game_id = @GameId", connection);
                command.Parameters.AddWithValue("@GameId", gameId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new AddGameLog
                        {
                            GameId = Convert.ToInt32(reader["game_id"]),
                            
                        });
                    }
                }
            }
            return logs;
        }

        public List<AddGameLog> GetLogsByAdmin(int adminId)
        {
            var logs = new List<AddGameLog>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT game_id, admin_id FROM add_game WHERE admin_id = @AdminId", connection);
                command.Parameters.AddWithValue("@AdminId", adminId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new AddGameLog
                        {
                            GameId = Convert.ToInt32(reader["game_id"]),
                            AdminId = Convert.ToInt32(reader["admin_id"])
                        });
                    }
                }
            }
            return logs;
        }

        public bool Del(int gameId) 
        {
            bool successAddGame = false;
            bool successGame = false;

            try
            {
                // Delete from add_game
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new SqlCommand("DELETE FROM add_game WHERE game_id = @gameId", connection))
                {
                    command.Parameters.AddWithValue("@gameId", gameId);
                    connection.Open();
                    int rowsAffectedAddGame = command.ExecuteNonQuery(); 
                                                                         // You can check rowsAffectedAddGame if needed (e.g., > 0 means something was deleted)
                    successAddGame = true; 
                }

                // Delete from game
                using (var connection = DatabaseHelper.GetConnection())
                using (var command = new SqlCommand("DELETE FROM game WHERE game_id = @gameId", connection))
                {
                    command.Parameters.AddWithValue("@gameId", gameId);
                    connection.Open();
                    int rowsAffectedGame = command.ExecuteNonQuery(); 
                    if (rowsAffectedGame > 0) // At least one row in 'game' table was deleted
                    {
                        successGame = true;
                    }
                    else
                    {

                         successGame = false;
                    }
                }

                return successGame;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error during delete: " + ex.Message); // Log the error
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error during delete: " + ex.Message); // Log the error
                return false;
            }
        }
        // to do: 
        // 1- fry some potato 34an ana g3an
        //2 - 
        // Delete methods if needed
    }
}