using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess.Repositories
{
    interface IUserRepository : IRepository<UserDTO>
    {
    }
}
