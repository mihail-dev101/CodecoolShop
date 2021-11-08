using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class CartItemModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public CartItemModel()
        {
        }
    }
}
