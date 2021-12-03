using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class UserService
    {
        private readonly IUserDao userDao;


        public UserService(IUserDao userDao)
        {
            this.userDao = userDao;
        }
        public void AddUser(CheckoutModel user)
        {
            this.userDao.AddUser(user);
        }


        public CheckoutModel GetUserIfValid(SigninModel model)
        {
            return this.userDao.GetUserByCredentials(model.Email, model.Password);

        }
        
    }
}
