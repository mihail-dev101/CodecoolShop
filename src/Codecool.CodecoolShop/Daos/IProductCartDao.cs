using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductCartDao: IDao<CartItemModel>
    {
        IEnumerable<CartItemModel> GetCart();

        void RemoveItemFromCartTotally(int id);

        void EmptyCart();
    }
}
