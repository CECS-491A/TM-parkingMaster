using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Manager.Managers
{
    public class UserManagementManager
    {
        private IUserManagementService _userManagementService;
        private readonly DatabaseContext _databaseContext;

        public UserManagementManager()
        {
            _databaseContext = new DatabaseContext();
            _userManagementService = new UserManagementService(_databaseContext);
        }

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

        public bool DeleteUser(string email)
        {
            try
            {
                _userManagementService.DeleteUser(email);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

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


    }
}