using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserRepository
    {
        List<UserEntity> GetList();
        UserEntity GetById(int id);
        UserEntity CreateUser(UserEntity user);
        UserEntity UpdateUser(UserEntity user);
    }
}
