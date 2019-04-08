using System;
using System.Collections.Generic;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Services.Services
{
    public interface IUserManagementService
    {
        /*
        bool DeleteUser(User user);
        bool DeleteUser(string email);
        bool UpdateUser(User user);
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsers();
        void AddUserClaim(User user, Claim claim);
        */

        ResponseDTO<bool> CreateUser(UserAccount user, List<Claim> claims);
        ResponseDTO<UserAccountDTO> GetUserBySsoId(Guid id);
        ResponseDTO<UserAccountDTO> GetUserByUserId(Guid id);
    }
}
