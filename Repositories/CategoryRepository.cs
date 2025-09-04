using GameRentalSystem.Model.Entities;
using GameRentalSystem.Model.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GameRentalSystem.Model.Repositories
{
    public class CategoryRepository
    {
        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT category_id, category_name FROM category", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(MapCategoryFromReader(reader));
                    }
                }
            }
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = null;
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT category_id, category_name FROM category WHERE category_id = @CategoryId", connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        category = MapCategoryFromReader(reader);
                    }
                }
            }
            // if (category != null)
            // {
            //     category.Games = GetGamesForCategory(categoryId); // Example of populating navigation
            // }
            return category;
        }

        public bool AddCategory(Category category)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("INSERT INTO category (category_name) VALUES (@CategoryName)", connection);
                //command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateCategory(Category category)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = new SqlCommand("UPDATE category SET category_name = @CategoryName WHERE category_id = @CategoryId", connection);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteCategory(int categoryId)
        {

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var cmdGameCategory = new SqlCommand("DELETE FROM game_category WHERE category_id = @CategoryId", connection, transaction);
                        cmdGameCategory.Parameters.AddWithValue("@CategoryId", categoryId);
                        cmdGameCategory.ExecuteNonQuery();

                        var command = new SqlCommand("DELETE FROM category WHERE category_id = @CategoryId", connection, transaction);
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
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

        private Category MapCategoryFromReader(SqlDataReader reader)
        {
            return new Category
            {
                CategoryId = Convert.ToInt32(reader["category_id"]),
                CategoryName = reader["category_name"].ToString()
            };
        }
    }
}