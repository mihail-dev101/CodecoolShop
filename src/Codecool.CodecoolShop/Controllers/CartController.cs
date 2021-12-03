using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<CartController> _logger;
        public ProductService ProductService { get; set; }
        public UserService UserService { get; set; }
        public CartService CartService { get; set; }
        public CartController(ILogger<CartController> logger, IHostingEnvironment env)
        {
            _env = env;
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoDb.GetInstance(),
                ProductCategoryDaoDb.GetInstance(),
                SupplierDaoDb.GetInstance(),
                ProductCartDaoDb.GetInstance(),
                UserDaoDb.GetInstance());
            UserService = new UserService(UserDaoDb.GetInstance());
            CartService = new CartService(ProductCartDaoDb.GetInstance());
        }
        public IActionResult Cart()
        {
            List<CartItemModel> cart = new List<CartItemModel>();
            if (HttpContext.Request.Cookies["userId"] != null)
            {
                cart = CartService.GetCartForUser(Int32.Parse(HttpContext.Request.Cookies["userId"]));
            }
            else
            {
                cart = CartService.GetCart().ToList();
            }

            return View(cart);
        }
        [HttpPost]
        public JsonResult Cart(string content)
        {
            if (content != null)
            {
                CartService.EmptyShoppingCart();
                var cart = JsonConvert.DeserializeObject<CartDeserializeModel[]>(content);
                List<CartItemModel> cartItemModels = new List<CartItemModel>();
                foreach (var item in cart)
                {
                    CartItemModel cartItem = new CartItemModel();
                    Product product = new Product();
                    product.Id = Int32.Parse(item.id);
                    product.Name = item.name;
                    ProductCategory category = new ProductCategory();
                    category.Name = item.category;
                    product.ProductCategory = category;
                    Supplier supplier = new Supplier();
                    supplier.Name = item.supplier;
                    product.Supplier = supplier;
                    if (item.price == "$NaN")
                    {
                        product.DefaultPrice = 369.0m;
                    }
                    else
                    {
                        product.DefaultPrice = Decimal.Parse(item.price.Replace("$", ""));
                    }

                    product.Currency = "$";
                    cartItem.Product = product;
                    cartItem.Quantity = Int32.Parse(item.quanity);
                    cartItemModels.Add(cartItem);
                    CartService.AddCartItemToCart(cartItem);
                }

            }
            return Json(Url.Action("Cart"));

        }
    }
}
