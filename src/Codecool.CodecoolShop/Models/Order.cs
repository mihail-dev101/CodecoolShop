using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class Order
    {
        public (CheckoutModel, List<CartItemModel>) OrderDetails { get; set; }
        public Order()
        {

        }
    }
}
