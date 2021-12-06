using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class CartService
    {
        private readonly IProductCartDao productCartDao;


        public CartService(IProductCartDao productCartDao)
        {
            this.productCartDao = productCartDao;
        }
        public IEnumerable<CartItemModel> GetCart()
        {
            return this.productCartDao.GetCart();
        }
        public List<CartItemModel> GetCartForUser(int userId)
        {
            return this.productCartDao.GetUserCart(userId);
        }
        public void AddProductToCart(Product product)
        {
            CartItemModel cartItemModel = new CartItemModel();
            cartItemModel.Product = product;
            cartItemModel.Quantity = 1;
            this.productCartDao.Add(cartItemModel);
        }
        public void AddCartItemToCart(CartItemModel item)
        {
            this.productCartDao.Add(item);
        }

        public void RemoveFromCart(Product product)
        {
            this.productCartDao.Remove(product.Id);
        }

        public void RemoveItemFromCartTotally(Product product)
        {
            this.productCartDao.RemoveItemFromCartTotally(product.Id);
        }

        public void EmptyShoppingCart(int? userId)
        {
            this.productCartDao.EmptyCart(userId);
        }
    }
}
