using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao productSupplierDao;
        private readonly IProductCartDao productCartDao;
        private readonly IUserDao userDao;


        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao, IProductCartDao productCartDao, IUserDao userDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.productSupplierDao = supplierDao;
            this.productCartDao = productCartDao;
            this.userDao = userDao;
        }
        

        public IEnumerable<(CheckoutModel, List<CartItemModel>)> GetAllOrders()
        {
            return this.userDao.GetAll();
        }

        public void AddOrder(Order order)
        {
            this.userDao.Add((order.OrderDetails.Item1,order.OrderDetails.Item2));
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.productDao.GetAll();
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(ProductCategory productCategory)
        {
            ProductCategory category = this.productCategoryDao.Get(productCategory.Id);
            return this.productDao.GetBy(category);
        }

        public Supplier GetProductSupplier(int supplierId)
        {
            return this.productSupplierDao.Get(supplierId);
        }

        public IEnumerable<Product> GetProductsForSupplier(Supplier supplier)
        {
            Supplier productSupplier = this.productSupplierDao.Get(supplier.Id);
            return this.productDao.GetBy(productSupplier);
        }


        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return this.productSupplierDao.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            return this.productCategoryDao.GetAll();
        }

        public Product GetProduct(int id)
        {
            return this.productDao.Get(id);
        }
        

        
    }
}
