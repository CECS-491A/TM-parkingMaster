using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess.Repositories
{
    class UserRepository : Repository<UserDTO>, IUserRepository
    {
        public UserRepository() : base(new UserDBEntities()) // ... will fix later
        {

        }
    }
}
