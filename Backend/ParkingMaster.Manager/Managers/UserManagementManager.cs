using System;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Manager.Managers
{
    public class UserManagementManager
    {
        
        private UserManagementService _userManagementService;

        public UserManagementManager()
        {
            _userManagementService = new UserManagementService();
        }
        /*
        // I am considering moving context.SaveChanges() here, but maybe later

        public bool CreateUser(string email, string password, string dob, string city, string state, string country, string role, bool act)
        {
            try
            {
                User user = new User
                {
                    Email = email,
                    Password = password,
                    DateOfBirth = dob,
                    City = city,
                    State = state,
                    Country = country,
                    Role = role,
                    Activated = act
                };
                _userManagementService.CreateUser(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        */
        public bool DeleteUser(Guid userId)
        {
            try
            {
                _userManagementService.DeleteUser(userId);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        /*
        public bool UpdateUser(User user)
        {
            try
            {
                _userManagementService.UpdateUser(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userManagementService.GetAllUsers();
        }

    */
    }
}