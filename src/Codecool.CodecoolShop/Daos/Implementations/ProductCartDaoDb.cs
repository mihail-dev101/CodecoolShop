using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductCartDaoDb : IProductCartDao 
    {
        DbProviderFactory factory;
        string provider;
        string connectionString;

        public ProductCartDaoDb(string connectionString)
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }

        public void Add(Product item)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"Insert Into product (name, supplier, description, price, category, currency ) Values ('{item.Name}', '{item.Supplier}', '{item.Description}'), '{item.DefaultPrice}'), '{item.ProductCategory}'), '{item.Currency}');";
                command.ExecuteNonQuery();
            }
        }

        public void Add(CartItemModel item)
        {
            throw new NotImplementedException();
        }

        public void EmptyCart()
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            var products = new List<Product>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"Select * From product WHERE id = {id};";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        (
                            (string)reader["name"],
                            (string)reader["description"],
                            (string)reader["currency"],
                            (decimal)reader["price"],
                            (ProductCategory)reader["category"],
                            (Supplier)reader["supplier"]
                        ));
                    }
                }
            }
            return products[0];
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select * From product;";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        (
                            (string)reader["name"],
                            (string)reader["description"],
                            (string)reader["currency"],
                            (decimal)reader["price"],
                            (ProductCategory)reader["category"],
                            (Supplier)reader["supplier"]
                        ));
                    }
                }
            }
            return products;
        }

        public IEnumerable<CartItemModel> GetCart()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveItemFromCartTotally(int id)
        {
            throw new NotImplementedException();
        }

        CartItemModel IDao<CartItemModel>.Get(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<CartItemModel> IDao<CartItemModel>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
