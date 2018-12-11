using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Services.Services
{
    public class UserManagementService : IUserManagementService
    {
        private UserRepository _userRepository;

        public UserManagementService(DatabaseContext databaseContext)
        {
            _userRepository = new UserRepository(databaseContext);
        }

        public bool CreateUser(User user)
        {
            try
            {
                if (_userRepository.UserExists(user.Email))
                {
                    Console.WriteLine("User with that email already exists.");
                    return false;
                }
                _userRepository.Insert(user);
                return true;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                if (!(_userRepository.UserExists(user.Email)))
                {
                    Console.WriteLine("User does not exist.");
                    return false;
                }
                User deleteUser = _userRepository.GetByEmail(user.Email);
                _userRepository.Delete(deleteUser);
                return true;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public bool DeleteUser(string email)
        {
            try
            {
                if (!(_userRepository.UserExists(email)))
                {
                    Console.WriteLine("User does not exist.");
                    return false;
                }
                User deleteUser = _userRepository.GetByEmail(email);
                _userRepository.Delete(deleteUser);
                return true;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                if (!(_userRepository.UserExists(user.Email)))
                {
                    Console.WriteLine("User does not exist.");
                    return false;
                }
                User updateUser = _userRepository.GetByEmail(user.Email);
                _userRepository.Update(updateUser);
                return true;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
    }
}
