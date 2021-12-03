using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Web.Services3.Security.Utility;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }
        public UserService UserService { get; set; }
        public CartService CartService { get; set; }
        public ProductController(ILogger<ProductController> logger, IHostingEnvironment env)
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
                    if(item.price == "$NaN")
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
            
            } return Json(Url.Action("Cart"));
            
        }
        public IActionResult Index()
        {

            var model = new IndexModel();
            var products = ProductService.GetAllProducts();
            var categories = ProductService.GetAllCategories();
            var suppliers = ProductService.GetAllSuppliers();
            
            model.Products = products.ToList();
            model.ProductSuppliers = suppliers.ToList();
            model.ProductCategories = categories.ToList();
            return View(model);
            
        }

        public IActionResult ProductsByCategory(int id)
        {
            var model = new IndexModel();
            var suppliers = ProductService.GetAllSuppliers();
            var categories = ProductService.GetAllCategories();
            foreach(var category in categories)
            {
                if(category.Id == id)
                {
                    var productsForCategory = ProductService.GetProductsForCategory(category);
                    model.Products = productsForCategory.ToList();
                }
            }
            model.ProductSuppliers = suppliers.ToList();
            return View("Index", model);
        }

        public IActionResult ProductsBySupplier(int id)
        {
            var model = new IndexModel();
            var categories = ProductService.GetAllCategories();
            var suppliers = ProductService.GetAllSuppliers();
            foreach (var supplier in suppliers)
            {
                if (supplier.Id == id)
                {
                    var productsForSupplier = ProductService.GetProductsForSupplier(supplier);
                    model.Products = productsForSupplier.ToList();
                }
            }
            model.ProductCategories = categories.ToList();
            return View("Index", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MoneyBack()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutModel user)
        {
            if (ModelState.IsValid)
            {
                var cart = CartService.GetCart().ToList();
                var order = new Order();
                order.OrderDetails = (user, cart);
                ProductService.AddOrder(order);
                return RedirectToAction("Payment");

                EmailSender.Execute(user.Email);
            }
            return View();
        }

        public IActionResult Payment()
        {
            
            PaymentModel payment = new PaymentModel();
            payment.Cart = CartService.GetCart().ToList();

            return View(payment);
        }

        [HttpPost]
        public IActionResult Payment(PaymentModel paymentModel)
        {
            
            if (ModelState.IsValid)
            {
                OrderFinalize();
                return RedirectToAction("Index");
            }
            string name = paymentModel.Name;
            string monthYear = paymentModel.MonthYear;
            int cvv = paymentModel.CVV;
            string cardNumber = paymentModel.CardNumber;
            return View(paymentModel);
        }
        private IHostingEnvironment _env;
        public void OrderFinalize()
        {
            //string
            //EmailSender.Execute();

            var orders = ProductService.GetAllOrders().ToList();
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "json.json");
            string jsonData = JsonConvert.SerializeObject(orders[orders.Count-1], Formatting.Indented);
            System.IO.File.WriteAllText(file, jsonData);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Register(RegistrationModel registerDetails)
        {

            if (ModelState.IsValid)
            {
                CheckoutModel userDetails = new CheckoutModel();
                userDetails.BuyerName = registerDetails.Name;
                userDetails.Email = registerDetails.Email;
                userDetails.Password = registerDetails.Password;
                UserService.AddUser(userDetails);
                ViewBag.Message = "User Details Saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Register");
            }
        }
        public ActionResult Signin()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signin(SigninModel model)
        {
            var user = UserService.GetUserIfValid(model);
            if (ModelState.IsValid && user != null)
            {

                ViewBag.Message = $"Welcome {model.Email}";
                HttpContext.Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions { Expires = DateTime.Now.AddHours(3) });
                HttpContext.Response.Cookies.Append("userName", user.BuyerName, new CookieOptions { Expires = DateTime.Now.AddHours(3) });
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Wrong credentials";
                return View("Login");
            }
        }
        //public ActionResult Signin(SigninModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CheckoutModel userDetails = new CheckoutModel();
        //        userDetails.Email = model.Email;
        //        userDetails.Password = model.Password;
        //        if(ProductService.IsValidUser(model) != null)
        //        {
        //            ViewBag.Message = "User Details Saved";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Wrong credentials";
        //            return View("Login");
        //        }
        //    }
        //    return View("Login");
        //}


        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            //Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index");
        }
    }
}

