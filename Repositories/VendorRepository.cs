using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class VendorRepository
    {
        public List<Vendor> GetAllVendors()
        {
            var vendors = new List<Vendor>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT vendor_id, vendName, phone FROM vendor", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vendor = MapVendorFromReader(reader);
                        // vendor.AdditionalPhones = GetVendorPhones(vendor.VendorId, connection); // Pass open connection or reopen
                        vendors.Add(vendor);
                    }
                }
            }
            // Populate additional phones for all vendors if needed in the list
            foreach (var v in vendors)
            {
                v.AdditionalPhones = GetVendorPhones(v.VendorId);
            }
            return vendors;
        }

        public Vendor GetVendorById(int vendorId)
        {
            Vendor vendor = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT vendor_id, vendName, phone FROM vendor WHERE vendor_id = @VendorId", connection);
                command.Parameters.AddWithValue("@VendorId", vendorId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vendor = MapVendorFromReader(reader);
                    }
                }
            }
            if (vendor != null)
            {
                vendor.AdditionalPhones = GetVendorPhones(vendorId);
                // vendor.DevelopedGames = GetDevelopedGamesByVendor(vendorId); // Example
            }
            return vendor;
        }

        public bool AddVendor(Vendor vendor, out int generatedVendorId)
        {
            generatedVendorId = 0;
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert into vendor table and get the generated ID
                        string query = "INSERT INTO vendor (vendName, phone) VALUES (@VendName, @PrimaryPhone); SELECT SCOPE_IDENTITY();";
                        var command = new SqlCommand(query, connection, transaction);
                        command.Parameters.AddWithValue("@VendName", vendor.VendorName);
                        command.Parameters.AddWithValue("@PrimaryPhone", vendor.PrimaryPhone);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            generatedVendorId = Convert.ToInt32(result);
                            vendor.VendorId = generatedVendorId; // Update the vendor object
                        }
                        else
                        {
                            transaction.Rollback();
                            return false; // Failed to get new vendor ID
                        }

                        // Insert additional phones if any, using the generatedVendorId
                        foreach (var phone in vendor.AdditionalPhones)
                        {
                            var phoneCommand = new SqlCommand("INSERT INTO vendor_phone (vendor_id, phone_num) VALUES (@VendorId, @PhoneNum)", connection, transaction);
                            phoneCommand.Parameters.AddWithValue("@VendorId", generatedVendorId); // Use the ID from idenity
                            phoneCommand.Parameters.AddWithValue("@PhoneNum", phone.PhoneNumber);
                            phoneCommand.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error adding vendor: " + ex.Message); // Log error
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }


        public bool UpdateVendor(Vendor vendor)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand("UPDATE vendor SET vendName = @VendName, phone = @PrimaryPhone WHERE vendor_id = @VendorId", connection, transaction);
                        command.Parameters.AddWithValue("@VendName", vendor.VendorName);
                        command.Parameters.AddWithValue("@PrimaryPhone", vendor.PrimaryPhone);
                        command.Parameters.AddWithValue("@VendorId", vendor.VendorId);
                        command.ExecuteNonQuery();

                        var deletePhonesCommand = new SqlCommand("DELETE FROM vendor_phone WHERE vendor_id = @VendorId", connection, transaction);
                        deletePhonesCommand.Parameters.AddWithValue("@VendorId", vendor.VendorId);
                        deletePhonesCommand.ExecuteNonQuery();

                        foreach (var phone in vendor.AdditionalPhones)
                        {
                            var phoneCommand = new SqlCommand("INSERT INTO vendor_phone (vendor_id, phone_num) VALUES (@VendorId, @PhoneNum)", connection, transaction);
                            phoneCommand.Parameters.AddWithValue("@VendorId", vendor.VendorId);
                            phoneCommand.Parameters.AddWithValue("@PhoneNum", phone.PhoneNumber);
                            phoneCommand.ExecuteNonQuery();
                        }
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

        public bool DeleteVendor(int vendorId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete from develop table (games developed by this vendor)
                        var cmdDevelop = new SqlCommand("DELETE FROM develop WHERE vendor_id = @VendorId", connection, transaction);
                        cmdDevelop.Parameters.AddWithValue("@VendorId", vendorId);
                        cmdDevelop.ExecuteNonQuery();

                        // Delete additional phones
                        var cmdPhones = new SqlCommand("DELETE FROM vendor_phone WHERE vendor_id = @VendorId", connection, transaction);
                        cmdPhones.Parameters.AddWithValue("@VendorId", vendorId);
                        cmdPhones.ExecuteNonQuery();

                        // Delete vendor
                        var command = new SqlCommand("DELETE FROM vendor WHERE vendor_id = @VendorId", connection, transaction);
                        command.Parameters.AddWithValue("@VendorId", vendorId);
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

        public List<VendorPhone> GetVendorPhones(int vendorId) // SqlConnection can be passed if already open
        {
            var phones = new List<VendorPhone>();
            using (var connection = DatabaseHelper.GetConnection()) 
            {
                var command = new SqlCommand("SELECT vendor_id, phone_num FROM vendor_phone WHERE vendor_id = @VendorId", connection);
                command.Parameters.AddWithValue("@VendorId", vendorId);
                if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        phones.Add(new VendorPhone
                        {
                            VendorId = Convert.ToInt32(reader["vendor_id"]),
                            PhoneNumber = reader["phone_num"].ToString()
                        });
                    }
                }
            }
            return phones;
        }

        private Vendor MapVendorFromReader(SqlDataReader reader)
        {
            return new Vendor
            {
                VendorId = Convert.ToInt32(reader["vendor_id"]),
                VendorName = reader["vendName"].ToString(),
                PrimaryPhone = reader["phone"]?.ToString() // Nullable if phone can be null
            };
        }
    }
}