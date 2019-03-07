using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Services.Services
{
    public interface IUserManagementService
    {
        bool CreateUser(User user);
        bool DeleteUser(User user);
        bool DeleteUser(string email);
        bool UpdateUser(User user);
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsers();
        void AddUserClaim(User user, Claim claim);
    }
}
