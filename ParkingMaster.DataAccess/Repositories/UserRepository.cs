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
        public UserRepository() : base(new UserContext())
        {

        }
    }
}
