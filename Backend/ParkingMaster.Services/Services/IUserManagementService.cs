using System;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Services.Services
{
    public interface IUserManagementService
    {
        /*
        bool CreateUser(User user);
        bool DeleteUser(User user);
        bool DeleteUser(string email);
        bool UpdateUser(User user);
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsers();
        void AddUserClaim(User user, Claim claim);
        */
        ResponseDTO<UserAccountDTO> GetUserBySsoId(Guid id);
        ResponseDTO<UserAccountDTO> GetUserByUserId(Guid id);
    }
}
