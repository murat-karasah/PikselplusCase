using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        public UserEntity CreateUser(UserEntity user)
        {
            using(var db = new PikselDbContext())
            {
                user.Deposit = 30;
                db.UserEntities.Add(user);
                db.SaveChanges();
                return user;
            }
        }

        public UserEntity GetById(int id)
        {
            using (var db = new PikselDbContext())
            {
                return db.UserEntities.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<UserEntity> GetList()
        {
            using (var db = new PikselDbContext())
            {
                return db.UserEntities.ToList();
            }
        }

        public UserEntity UpdateUser(UserEntity user)
        {
            using (var db = new PikselDbContext())
            {
                db.UserEntities.Update(user);
                db.SaveChanges();
                return user;
            }
        }
    }
}
