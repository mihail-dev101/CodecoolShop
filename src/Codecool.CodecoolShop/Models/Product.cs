using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Models
{
    public class Product : BaseModel
    {
        public Product()
        {
        }

        public Product(string name, string description, string currency, decimal price, ProductCategory category, Supplier supplier) : base()
        {
            Name = name;
            Description = description;
            Currency = currency;
            DefaultPrice = price;
            ProductCategory = category;
            Supplier = supplier;
        }

        public string Currency { get; set; }
        public decimal DefaultPrice { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public Supplier Supplier { get; set; }

        

        public void SetProductCategory(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
            ProductCategory.Products.Add(this);
        }
    }
}
