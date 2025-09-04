using System;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Database
{
    public static class DatabaseHelper
    {
        //
        //private static string _connectionString = @"Data Source=(LocalDB)\KMSSWLSERVER;AttachDbFilename=C:\Users\princ\AppData\Local\Temp\~vs9E1A.sql;Integrated Security=True;";
        private static string _connectionString = @"Data Source=Abdullah;Initial Catalog=Game_Rent;Integrated Security=True;";
        public static string ConnectionString => _connectionString;

        public static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(_connectionString);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}