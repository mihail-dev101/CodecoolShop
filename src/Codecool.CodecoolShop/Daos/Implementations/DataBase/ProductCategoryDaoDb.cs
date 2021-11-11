using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductCategoryDaoDb : IProductCategoryDao
    {
        DbProviderFactory factory;
        string provider;
        string connectionString;
        private static ProductCategoryDaoDb instance = null;

        private ProductCategoryDaoDb()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }

        public static ProductCategoryDaoDb GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductCategoryDaoDb();
            }

            return instance;
        }

        public void Add(ProductCategory item)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"Insert Into product ( name, description, department ) Values ('{item.Name}', '{item.Description}'), '{item.Department}');";
                command.ExecuteNonQuery();
            }
        }

        public ProductCategory Get(int id)
        {
            var categories = new List<ProductCategory>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select * From category " +
                    $"WHERE ID = {id};";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        var category = new ProductCategory();
                        category.Id = (int)reader["ID"];
                        category.Name = (string)reader["name"];
                        category.Description = (string)reader["description"];
                        category.Department = (string)reader["department"];
                        
                        categories.Add(category);
                    }
                }
            }
            return categories[0];
        }
    

        public IEnumerable<ProductCategory> GetAll()
        {
            var categories = new List<ProductCategory>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select * From category ";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var category = new ProductCategory();
                        category.Id = (int)reader["ID"];
                        category.Name = (string)reader["name"];
                        category.Description = (string)reader["description"];
                        category.Department = (string)reader["department"];

                        category.Id = (int)reader["ID"];
                        categories.Add(category);
                    }
                }
            }
            return categories;
        }
        
    

        public void Remove(int id)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM category WHERE ID = {id}";
                command.ExecuteNonQuery();
            }
        }
    }
}
