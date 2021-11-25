using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDaoDb : IUserDao
    {
        DbProviderFactory factory;
        string provider;
        string connectionString;
        private static UserDaoDb instance = null;
        private UserDaoDb()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }
        public static UserDaoDb GetInstance()
        {
            if (instance == null)
            {
                instance = new UserDaoDb();
            }
            return instance;
        }
        public void AddUser(CheckoutModel item)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO [user] (name, password, email, phone_number, address, city, country, zipcode) " + 
                    $"VALUES ('{item.BuyerName}','{item.Password}','{item.Email}', '{item.PhoneNumber}', '{item.Address}', '{item.City}', '{item.Country}', '{item.ZipCode}');";
                command.ExecuteNonQuery();
            }

         }

        public void UpdateUser(CheckoutModel item)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE [user] SET name = '{item.BuyerName}', email = '{item.Email}', phone_number = '{item.PhoneNumber}', city = '{item.City}', country = '{item.Country}', zipcode = '{item.ZipCode}' " +
                    $"WHERE ID = {item.Id};";
                command.ExecuteNonQuery();
            }
        }

        public (CheckoutModel, List<CartItemModel>) Get(int id)
        {
            var usersData = new List<(CheckoutModel, List<CartItemModel>)>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT *, cart.ID as 'cart.ID', cart.product_id as 'cart.product_id', cart.quantity as 'cart.quantity' FROM [user]" +
                    "JOIN cart ON cart.user_id = user.ID" +
                    $"WHERE user.ID = {id}";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    CheckoutModel checkout = new CheckoutModel();
                    checkout.Id = (int)reader["ID"];
                    checkout.PhoneNumber = (string)reader["phone_number"];
                    checkout.ZipCode = Int32.Parse((string)reader["zipcode"]);
                    checkout.Email = (string)reader["email"];
                    checkout.Country = (string)reader["country"];
                    checkout.City = (string)reader["city"];
                    checkout.BuyerName = (string)reader["name"];
                    checkout.Password = (string)reader["password"];
                    checkout.Address = (string)reader["address"];
                    List<CartItemModel> cart = ProductCartDaoDb.GetInstance().GetUserCart(checkout.Id);
                    usersData.Add((checkout, cart));
                }
            }
            return usersData[0];
        }

        public IEnumerable<(CheckoutModel, List<CartItemModel>)> GetAll()
        {
            var usersData = new List<(CheckoutModel, List<CartItemModel>)>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT *, cart.ID as 'cart.ID', cart.product_id as 'cart.product_id', cart.quantity as 'cart.quantity' FROM [user]" +
                    "JOIN cart ON cart.user_id = user.ID";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    CheckoutModel checkout = new CheckoutModel();
                    checkout.Id = (int)reader["ID"];
                    checkout.PhoneNumber = (string)reader["phone_number"];
                    checkout.ZipCode = Int32.Parse((string)reader["zipcode"]);
                    checkout.Email = (string)reader["email"];
                    checkout.Country = (string)reader["country"];
                    checkout.City = (string)reader["city"];
                    checkout.BuyerName = (string)reader["name"];
                    checkout.Password = (string)reader["password"];
                    checkout.Address = (string)reader["address"];
                    List<CartItemModel> cart = ProductCartDaoDb.GetInstance().GetUserCart(checkout.Id);
                    usersData.Add((checkout, cart));
                }
            }
            return usersData;
        }

        public void Remove(int id)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM [user] WHERE user.ID = {id}";
                command.ExecuteNonQuery();
                command.CommandText = $"DELETE FROM cart WHERE cart.user_id = {id}";
                command.ExecuteNonQuery();
            }
        }

        public void Add((CheckoutModel, List<CartItemModel>) item)
        {
            throw new NotImplementedException();
        }

        public CheckoutModel GetUserByCredentials(string email, string password)
        {
            var users = new List<CheckoutModel>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [user] " +
                                      $"WHERE CONVERT(VARCHAR(MAX), [user].email) = '{email}' and CONVERT(VARCHAR(MAX), [user].password) = '{password}'";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) {
                        CheckoutModel user = new CheckoutModel();
                        user.Id = (int)reader["ID"];
                        user.PhoneNumber = reader["phone_number"]?.ToString()??"";
                        user.ZipCode = Int32.Parse(reader["zipcode"]?.ToString() ?? "");
                        user.Email = reader["email"]?.ToString() ?? "";
                        user.Country = reader["country"]?.ToString() ?? "";
                        user.City = reader["city"]?.ToString() ?? "";
                        user.BuyerName = reader["name"]?.ToString() ?? "";
                        user.Password = reader["password"]?.ToString() ?? "";
                        user.Address = reader["address"]?.ToString() ?? "";
                        users.Add(user);
                    }
                }
            }
            return users[0];
        }
    }
}
