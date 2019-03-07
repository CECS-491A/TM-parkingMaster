using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess;
using System.Reflection;


namespace ParkingMaster.Services
{
    /// <summary>
    /// Check nullity of object parameters. 
    /// Otherwise, return false.
    /// </summary>
    public class UserCreationService
    {
        /*
        private IUserRepository _repository;

        public UserCreationService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Boolean UserCreation(User user)
        {

            if (user != null) // check object is null
            {
                try
                {
                    _repository.Insert(user); // UserRepository.AddUser(user)
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception encountered: " + e.ToString());
                    return false;
                }
            }
            return false; // object is null
        }
        */
    }
}
