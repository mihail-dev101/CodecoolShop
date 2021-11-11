using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoDb : ISupplierDao
    {
        DbProviderFactory factory;
        string provider;
        string connectionString;
        private static SupplierDaoDb instance = null;

        public SupplierDaoDb()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }

        public static SupplierDaoDb GetInstance()
        {
            if (instance == null)
            {
                instance = new SupplierDaoDb();
            }
            return instance;
        }

        public void Add(Supplier item)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"Insert Into supplier ( name, description ) Values ('{item.Name}', '{item.Description}');";
                command.ExecuteNonQuery();
            }
        }

        public Supplier Get(int id)
        {
            var suppliers = new List<Supplier>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select * From supplier " +
                    $"WHERE ID = {id};";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier = new Supplier();
                        supplier.Id = (int)reader["ID"];
                        supplier.Name = (string)reader["name"];
                        supplier.Description = (string)reader["description"];
                        suppliers.Add(supplier);
                    }
                }
                return suppliers[0];
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            var suppliers = new List<Supplier>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select * From supplier ";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier = new Supplier();
                        supplier.Id = (int)reader["ID"];
                        supplier.Name = (string)reader["name"];
                        supplier.Description = (string)reader["description"];
                        suppliers.Add(supplier);
                    }
                }
            }
            return suppliers;
        }
    

        public void Remove(int id)
        {
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM supplier WHERE ID = {id}";
                command.ExecuteNonQuery();
            }
        }
    }
}
