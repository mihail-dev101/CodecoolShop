using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class IndexModel
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Supplier> ProductSuppliers { get; set; }
        public List<Product> ProductsForSupplier { get; set; }
        public List<Product> ProductsForCategory { get; set; }

        public IndexModel()
        {
            Products = new List<Product>();
            ProductsForSupplier = new List<Product>();
            ProductsForCategory = new List<Product>();
            ProductCategories = new List<ProductCategory>();
            ProductSuppliers = new List<Supplier>();
        }
    }
}
