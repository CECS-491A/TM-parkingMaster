using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.DataAccess.Models;
using System.Reflection;



namespace ParkingMaster.Services
{
    /// <summary>
    /// Check nullity of object parameters. 
    /// If all parameters non-null, add to database and return true.
    /// Otherwise, return false.
    /// </summary>
    public class UserCreationService
    {
        private UserRepository _repository;

        // When UserCreationService is instantiated, a UserRepository must have also been instantiated beforehand
        public UserCreationService(UserRepository repository)
        {
            _repository = repository;
        }

        public Boolean UserCreation(User user)
        {
            if (user != null) // check object is null
            {
                //var properties = user.GetType().GetProperties();
                //foreach (var p in properties)
                //{
                //    if (p.GetValue(user) == null) { return false; } // if any values null, cannot add. Reconsidering this?
                //}
                _repository.Insert(user); // UserRepository.AddUser(user)
                return true;
            }
            return false; // object is null
        }
    }
}
