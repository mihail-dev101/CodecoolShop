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

        public ProductCartDaoDb()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
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
            throw new NotImplementedException();
        }
    }
}
