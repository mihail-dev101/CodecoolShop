using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<UserController> _logger;
        public ProductService ProductService { get; set; }
        public UserService UserService { get; set; }
        public CartService CartService { get; set; }
        public UserController(ILogger<UserController> logger, IHostingEnvironment env)
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
        [HttpPost]
        public ActionResult Signin(SigninModel model)
        {
            var user = UserService.GetUserIfValid(model);
            if (ModelState.IsValid && user != null)
            {

                ViewBag.Message = $"Welcome {model.Email}";
                HttpContext.Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions { Expires = DateTime.Now.AddHours(3) });
                HttpContext.Response.Cookies.Append("userName", user.BuyerName, new CookieOptions { Expires = DateTime.Now.AddHours(3) });
                return RedirectToAction("Index","Product");
            }
            else
            {
                ViewBag.Message = "Wrong credentials";
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("userId");
            HttpContext.Response.Cookies.Delete("userName");
            //FormsAuthentication.SignOut();
            //Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index","Product");
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
                return RedirectToAction("Index","Product");
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
    }
}
