﻿using Codecool.CodecoolShop.Models;
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
        public void Add((CheckoutModel, List<CartItemModel>) item)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO user (name, password, email, phone_number, address, city, country, zipcode)" +
                    $"VALUES ({item.Item1.BuyerName},{item.Item1.Password},{item.Item1.Email},{item.Item1.PhoneNumber},{item.Item1.Address},{item.Item1.City},{item.Item1.Country},{item.Item1.ZipCode});";
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
                command.CommandText = "SELECT *, cart.ID as 'cart.ID', cart.product_id as 'cart.product_id', cart.quantity as 'cart.quantity' FROM user" +
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
                command.CommandText = "SELECT *, cart.ID as 'cart.ID', cart.product_id as 'cart.product_id', cart.quantity as 'cart.quantity' FROM user" +
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
                command.CommandText = $"DELETE FROM user WHERE user.ID = {id}";
                command.ExecuteNonQuery();
                command.CommandText = $"DELETE FROM cart WHERE cart.user_id = {id}";
                command.ExecuteNonQuery();
            }
        }
    }
}
