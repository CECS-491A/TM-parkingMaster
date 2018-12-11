using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess.Repositories
{
    
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            context = databaseContext;
            dbset = context.Set<User>();
        }

        public User GetByEmail(string email)
        {
            return dbset.Find(email);
        }

        public bool UserExists(string email)
        {
            if (GetByEmail(email) == null)
            {
                return false;
            }
            return true;
        }

        public User GetByEmail(User user)
        {
            return dbset.Find(user.Email);
        }

        public bool UserExists(User user)
        {
            if (GetByEmail(user.Email) == null)
            {
                return false;
            }
            return true;
        }

    }
}
