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
        private static ProductCartDaoDb instance = null;
        private ProductCartDaoDb()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }

        public static ProductCartDaoDb GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductCartDaoDb();
            }
            return instance;
        }

        public void Add(CartItemModel item)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO cart (product_id, user_id, quantity)" +
                    $"VALUES('{item.Product.Id}',{item.UserId},{item.Quantity});";
                command.ExecuteNonQuery();
            }
        }

        public void EmptyCart(int? user_id = 0)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM cart WHERE user_id = {user_id} ";
                command.ExecuteNonQuery();
            }
        }

        
        public IEnumerable<CartItemModel> GetCart()
        {
            var cart = new List<CartItemModel>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select * From cart";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CartItemModel item = new CartItemModel();
                        int productId = Int32.Parse((string)reader["product_id"]);
                        item.Product = ProductDaoDb.GetInstance().Get(productId);
                        item.Quantity = (int)reader["quantity"];
                        item.UserId = (int)reader["user_id"];
                        cart.Add(item);
                    }
                }
            }
            return cart;
        }

        public void Remove(int id, int? user_id=null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE cart SET quantity = quantity - 1 WHERE user_id = {user_id} AND product_id = {id}";
                command.ExecuteNonQuery();
            }
        }

        public void RemoveItemFromCartTotally(int id, int? user_id=null)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM cart WHERE user_id = {user_id} AND product_id = {id}";
                command.ExecuteNonQuery();
            }
        }

        public List<CartItemModel> GetUserCart(int user_id)
        {
            var cart = new List<CartItemModel>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"Select * From cart Where user_id = {user_id}";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CartItemModel item = new CartItemModel();
                        int productId = Int32.Parse((string)reader["product_id"]);
                        item.Product = ProductDaoDb.GetInstance().Get(productId);
                        item.Quantity = (int)reader["quantity"];
                        item.UserId = (int)reader["user_id"];
                        cart.Add(item);
                    }
                }
            }
            return cart;
        }
        public CartItemModel Get(int id)
        {
            var cart = new List<CartItemModel>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"Select * From cart Where ID = {id}";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CartItemModel item = new CartItemModel();
                        int productId = Int32.Parse((string)reader["product_id"]);
                        item.Product = ProductDaoDb.GetInstance().Get(productId);
                        item.Quantity = (int)reader["quantity"];
                        item.UserId = (int)reader["user_id"];
                        cart.Add(item);
                    }
                }
            }
            return cart[0];
        }

        public IEnumerable<CartItemModel> GetAll()
        {
            var cart = new List<CartItemModel>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select * From cart";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CartItemModel item = new CartItemModel();
                        int productId = Int32.Parse((string)reader["product_id"]);
                        item.Product = ProductDaoDb.GetInstance().Get(productId);
                        item.Quantity = (int)reader["quantity"];
                        item.UserId = (int)reader["user_id"];
                        cart.Add(item);
                    }
                }
            }
            return cart;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
