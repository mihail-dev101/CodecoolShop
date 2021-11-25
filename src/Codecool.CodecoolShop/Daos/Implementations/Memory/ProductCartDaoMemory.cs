using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductCartDaoMemory: IProductCartDao
    {
        private List<CartItemModel> Cart = new List<CartItemModel>();
        private static ProductCartDaoMemory instance = null;
        private ProductCartDaoMemory()
        {

        }
        public static ProductCartDaoMemory GetInstance()
        {
            if(instance == null)
            {
                instance = new ProductCartDaoMemory();
            }
            return instance;
        }
        public void Add(Product item)
        {
            bool found = false;
            foreach (var pr in Cart)
            {
                if (item == pr.Product)
                {
                    pr.Quantity++;
                    found = true;
                }
            }
            if (found == false)
            {
                var cartItem = new CartItemModel();
                cartItem.Product = item;
                cartItem.Quantity = 1;
            }
        }

        public CartItemModel Get(int id)
        {
            foreach(var item in Cart)
            {
                if(item.Product.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public IEnumerable<CartItemModel> GetAll()
        {
            return Cart;
        }

        public IEnumerable<CartItemModel> GetCart()
        {
            return Cart;
        }


        public void Add(CartItemModel item)
        {
            bool found = false;
            foreach(var pr in Cart)
            {
                if (pr.Product == item.Product)
                {
                    pr.Quantity += item.Quantity;
                    found = true;
                }
            }
            if(found == false)
            {
                Cart.Add(item);
            }
        }

        public void Remove(int id)
        {
            foreach(var item in Cart.Reverse<CartItemModel>())
            {
                if (item.Product.Id == id && item.Quantity == 1)
                {
                    Cart.Remove(item);
                }
                else if(item.Product.Id == id && item.Quantity > 1)
                {
                    item.Quantity--;
                }
            }
        }

        public void RemoveItemFromCartTotally(int id,int? user_id = null)
        {
            CartItemModel productToRemove = null;
            foreach(var item in Cart)
            {
                if (item.Product.Id == id)
                {
                    productToRemove = item;
                    break;
                }
            }
            if (productToRemove != null)
            {
                Cart.Remove(productToRemove);
            }
            
        }

        public void EmptyCart(int? user_id = null)
        {
            Cart = new List<CartItemModel>();
        }

        public List<CartItemModel> GetUserCart(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
