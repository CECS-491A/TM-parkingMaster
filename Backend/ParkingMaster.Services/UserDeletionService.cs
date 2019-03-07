using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.Services
{
    public class UserDeletionService
    {
        private IUserRepository _repository;

        public UserDeletionService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Boolean UserDeletion(User user)
        {

            if (user != null) // check object is null
            {
                try
                {
                    _repository.Delete(user); // UserRepository.AddUser(user)
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


    }
}
