using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository rep;

        public UserService(IUserRepository _rep)
        {
            rep = _rep;
        }

        public UserEntity CreateUser(UserEntity user)
        {
            return rep.CreateUser(user);
        }

        public UserEntity GetById(int id)
        {
            return rep.GetById(id);
        }

        public List<UserEntity> GetList()
        {
            return rep.GetList();
        }

        public UserEntity UpdateUser(UserEntity user)
        {
            return rep.UpdateUser(user);
        }
    }
}
