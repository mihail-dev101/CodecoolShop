using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDaoMemory : IUserDao
    {
        private List<(CheckoutModel, List<CartItemModel>)> data = new List<(CheckoutModel, List<CartItemModel>)>();
        private static UserDaoMemory instance = null;

        private UserDaoMemory()
        {
        }

        public static UserDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new UserDaoMemory();
            }

            return instance;
        }

        public void Add((CheckoutModel, List<CartItemModel>) order)
        {
            order.Item1.Id = data.Count + 1;
            data.Add(order);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public (CheckoutModel,List<CartItemModel>) Get(int id)
        {
            return data.Find(x => x.Item1.Id == id);
        }

        public IEnumerable<(CheckoutModel,List<CartItemModel>)> GetAll()
        {
            return data;
        }

        public void AddUser(CheckoutModel user)
        {

        }

        public void UpdateUser(CheckoutModel user)
        {

        }

        public CheckoutModel GetUserByCredentials(string email, string password)
        {
            return null;
        }
    }
}

