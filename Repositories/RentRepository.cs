using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using GameRentalSystem.Model.Entities.Reports;

namespace GameRentalSystem.Model.Repositories
{
    public class RentRepository
    {
        public List<Rent> GetAllRentals()
        {
           
            var rentals = new List<Rent>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT client_id, game_id, rent_date, return_date, rent_cost FROM rent", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentals.Add(MapRentFromReader(reader));
                    }
                }
            }
            return rentals;
        }


        public Rent GetRental(int clientId, int gameId, DateTime rentDate)
        {
            Rent rental = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                
                string query = "SELECT client_id, game_id, rent_date, return_date, rent_cost FROM rent " +
                               "WHERE client_id = @ClientId AND game_id = @GameId AND rent_date = @RentDate";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientId", clientId);
                command.Parameters.AddWithValue("@GameId", gameId);
                command.Parameters.AddWithValue("@RentDate", rentDate.Date); // Match by date part
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        rental = MapRentFromReader(reader);
                    }
                }
            }
            return rental;
        }


        public List<Rent> GetRentalsByClientId(int clientId)
        {
           
            var rentals = new List<Rent>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT client_id, game_id, rent_date, return_date, rent_cost FROM rent WHERE client_id = @ClientId ORDER BY rent_date DESC", connection);
                command.Parameters.AddWithValue("@ClientId", clientId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentals.Add(MapRentFromReader(reader));
                    }
                }
            }
            return rentals;
        }

        public List<Rent> GetRentalsByGameId(int gameId)
        {
            
            var rentals = new List<Rent>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT client_id, game_id, rent_date, return_date, rent_cost FROM rent WHERE game_id = @GameId", connection);
                command.Parameters.AddWithValue("@GameId", gameId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentals.Add(MapRentFromReader(reader));
                    }
                }
            }
            return rentals;
        }

        public bool AddRental(Rent rental)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open(); // Open connection once
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Decrement stock
                        var stockCmd = new SqlCommand("UPDATE game SET stock = stock - 1 WHERE game_id = @GameId AND stock > 0", connection, transaction);
                        stockCmd.Parameters.AddWithValue("@GameId", rental.GameId);
                        int stockUpdated = stockCmd.ExecuteNonQuery();

                        if (stockUpdated == 0)
                        {
                            transaction.Rollback(); // Not enough stock or game not found
                            return false;
                        }

                        // 2. Insert rental record
                        var rentCmd = new SqlCommand("INSERT INTO rent (client_id, game_id, rent_date, return_date, rent_cost) VALUES (@ClientId, @GameId, @RentDate, @ReturnDate, @RentCost)", connection, transaction);
                        rentCmd.Parameters.AddWithValue("@ClientId", rental.ClientId);
                        rentCmd.Parameters.AddWithValue("@GameId", rental.GameId);
                        rentCmd.Parameters.AddWithValue("@RentDate", rental.RentDate.Date); // Store only date part
                        rentCmd.Parameters.AddWithValue("@ReturnDate", rental.ReturnDate.HasValue ? (object)rental.ReturnDate.Value.Date : DBNull.Value); // Store only date part
                        rentCmd.Parameters.AddWithValue("@RentCost", rental.RentCost);

                        int rentInserted = rentCmd.ExecuteNonQuery();

                        if (rentInserted > 0)
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
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error in AddRental: " + ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("General Error in AddRental: " + ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool UpdateRentalForReturn(int clientId, int gameId, DateTime rentDate, DateTime actualReturnDate)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string updateRentQuery = "UPDATE rent SET return_date = @ActualReturnDate " +
                                                 "WHERE client_id = @ClientId AND game_id = @GameId AND rent_date = @RentDate AND return_date IS NULL";
                        var updateRentCmd = new SqlCommand(updateRentQuery, connection, transaction);
                        updateRentCmd.Parameters.AddWithValue("@ActualReturnDate", actualReturnDate.Date); // Store only date part
                        updateRentCmd.Parameters.AddWithValue("@ClientId", clientId);
                        updateRentCmd.Parameters.AddWithValue("@GameId", gameId);
                        updateRentCmd.Parameters.AddWithValue("@RentDate", rentDate.Date); // Match by date part

                        int rentUpdated = updateRentCmd.ExecuteNonQuery();

                        if (rentUpdated == 0)
                        {
                            // Rental not found, already returned, or keys didn't match
                            transaction.Rollback();
                            return false;
                        }

                        // 2. Increment game stock
                        var stockCmd = new SqlCommand("UPDATE game SET stock = stock + 1 WHERE game_id = @GameId", connection, transaction);
                        stockCmd.Parameters.AddWithValue("@GameId", gameId);
                        stockCmd.ExecuteNonQuery(); // Assume this will succeed if game exists

                        transaction.Commit();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error in UpdateRentalForReturn: " + ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("General Error in UpdateRentalForReturn: " + ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }



        public bool UpdateRental(Rent rental)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {

                string query = "UPDATE rent SET rent_date = @RentDate, return_date = @ReturnDate, rent_cost = @RentCost " +
                               "WHERE client_id = @ClientId AND game_id = @GameId AND rent_date = @OriginalRentDate"; // Added OriginalRentDate
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RentDate", rental.RentDate.Date);
                command.Parameters.AddWithValue("@ReturnDate", rental.ReturnDate.HasValue ? (object)rental.ReturnDate.Value.Date : DBNull.Value);
                command.Parameters.AddWithValue("@RentCost", rental.RentCost);
                command.Parameters.AddWithValue("@ClientId", rental.ClientId);
                command.Parameters.AddWithValue("@GameId", rental.GameId);

                command.Parameters.AddWithValue("@OriginalRentDate", rental.RentDate.Date); 


                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }


        public bool DeleteRental(int clientId, int gameId, DateTime rentDate) 
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                // 1. Increment stock
                var stockCmd = new SqlCommand("UPDATE game SET stock = stock + 1 WHERE game_id = @GameId", connection);

                string query = "DELETE FROM rent WHERE client_id = @ClientId AND game_id = @GameId AND rent_date = @RentDate";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientId", clientId);
                command.Parameters.AddWithValue("@GameId", gameId);
                command.Parameters.AddWithValue("@RentDate", rentDate.Date);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public SalesSummary GetOverallSalesSummary()
        {
            SalesSummary summary = new SalesSummary { TotalRentals = 0, TotalRevenue = 0m };
            string query = @"
        SELECT 
            COUNT(*) AS TotalRentals, 
            ISNULL(SUM(rent_cost), 0) AS TotalRevenue 
        FROM rent;";

            using (var connection = DatabaseHelper.GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        summary.TotalRentals = Convert.ToInt32(reader["TotalRentals"]);
                        summary.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                    }
                }
            }
            return summary;
        }

        // 2. Get Most Rented Games (Top N by count)
        public List<PopularGame> GetMostRentedGames(int topN = 5)
        {
            var popularGames = new List<PopularGame>();

            string query = $@"
        SELECT TOP {topN}
            r.game_id AS GameId,
            g.gameName AS GameName,
            COUNT(r.game_id) AS RentalCount
        FROM rent r
        JOIN game g ON r.game_id = g.game_id
        GROUP BY r.game_id, g.gameName
        ORDER BY RentalCount DESC;";

            using (var connection = DatabaseHelper.GetConnection())
            using (var command = new SqlCommand(query, connection))
            {

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        popularGames.Add(new PopularGame
                        {
                            GameId = Convert.ToInt32(reader["GameId"]),
                            GameName = reader["GameName"].ToString(),
                            RentalCount = Convert.ToInt32(reader["RentalCount"])
                        });
                    }
                }
            }
            return popularGames;
        }

        // 3. Get Games Generating Most Revenue (Top N by sum of rent_cost)
        public List<RevenueByGame> GetTopRevenueGames(int topN = 5)
        {
            var topRevenueGames = new List<RevenueByGame>();
            string query = $@"
        SELECT TOP {topN}
            r.game_id AS GameId,
            g.gameName AS GameName,
            ISNULL(SUM(r.rent_cost), 0) AS TotalRevenue
        FROM rent r
        JOIN game g ON r.game_id = g.game_id
        GROUP BY r.game_id, g.gameName
        ORDER BY TotalRevenue DESC;";

            using (var connection = DatabaseHelper.GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                // If using TOP (@TopN)
                // command.Parameters.AddWithValue("@TopN", topN);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        topRevenueGames.Add(new RevenueByGame
                        {
                            GameId = Convert.ToInt32(reader["GameId"]),
                            GameName = reader["GameName"].ToString(),
                            TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                        });
                    }
                }
            }
            return topRevenueGames;
        }

        private Rent MapRentFromReader(SqlDataReader reader)
        {
            return new Rent
            {
                ClientId = Convert.ToInt32(reader["client_id"]),
                GameId = Convert.ToInt32(reader["game_id"]),
                RentDate = Convert.ToDateTime(reader["rent_date"]),
                ReturnDate = reader["return_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["return_date"]),
                RentCost = Convert.ToDecimal(reader["rent_cost"])
            };
        }
    }
}