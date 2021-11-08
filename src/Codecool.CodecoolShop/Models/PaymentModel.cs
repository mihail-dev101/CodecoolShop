using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class PaymentModel
    {
        public List<CartItemModel> Cart { get; set; }
        public List<CheckoutModel> OrderDetails { get; set; }
        [Required]
        [CreditCard(AcceptedCardTypes = CreditCardAttribute.CardType.All)]
        public string CardNumber { get; set; }
        [Required]
        public string MonthYear { get; set; }
        [Required]
        [Range(100, 1000)]
        public int CVV { get; set; }
        [Required]
        public string Name { get; set; }
     }
}
