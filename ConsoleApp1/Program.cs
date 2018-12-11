using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess;
using ParkingMaster.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var ctx = new DatabaseContext())
            //{
            //    var user = new User()
            //    {
            //        Email = "tnguyen@gmail.com",
            //        Password = "totallyhashedpw",
            //        DateOfBirth = "12/25/1996",
            //        City = "Carson",
            //        State = "CA",
            //        Country = "US",
            //        Role = "Standard",
            //        Activated = false
            //    };
            //}

            using (DatabaseContext _databaseContext = new DatabaseContext())
            {
                UserManagementService _userManagementService = new UserManagementService(_databaseContext);
                var user = new User()
                {
                    Email = "jonnynguyen@gmail.com",
                    Password = "totallyhashedpw",
                    DateOfBirth = "12/25/1996",
                    City = "Carson",
                    State = "CA",
                    Country = "US",
                    Role = "Standard",
                    Activated = false
                };
                _userManagementService.CreateUser(user);

                var all = _userManagementService.GetAllUsers();
                foreach (User thing in all)
                {
                    Console.WriteLine(thing.Email);
                }

                Console.ReadKey();
            }
                

            

        }
    }
}
