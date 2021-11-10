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
        private ProductCartDaoDb instance = null;
        private ProductCartDaoDb()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }

        public ProductCartDaoDb GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductCartDaoDb();
            }
            return instance;
        }

        public void Add(CartItemModel item)
        {
            throw new NotImplementedException();
        }

        public void EmptyCart()
        {
            throw new NotImplementedException();
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
            var cart = new List<CartItemModel>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select *, cart.ID as 'cart.ID',cart.user_ID as 'cart.user_ID',cart.product_id as 'cart.product_id' From product, cart";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CartItemModel item = new CartItemModel();
                        string allProducts = (string)reader["product_id"];
                        string[] productIds = allProducts.Split(",");
                        List<int> ids = new List<int>();
                        foreach(var id in productIds)
                        {
                            ids.Add(Int32.Parse(id));
                        }
                        item.UserId = (int)reader["user_id"];

                    }
                }
            }
            return null;
    }
    }
}
