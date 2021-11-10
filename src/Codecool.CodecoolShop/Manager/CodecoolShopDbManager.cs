using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Manager
{
    public class CodecoolShopDbManager
    {
        DbProviderFactory factory;
        string provider;
        string connectionString;

        public string ConnectionString => Connect();
        public CodecoolShopDbManager()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }

        public static string Connect()
        {
            return ConfigurationManager.AppSettings["connectionString"];
        }

        
    }
}
