using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDaoDb : IUserDao
    {
        public void Add((CheckoutModel, List<CartItemModel>) item)
        {
            throw new NotImplementedException();
        }

        public (CheckoutModel, List<CartItemModel>) Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<(CheckoutModel, List<CartItemModel>)> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
