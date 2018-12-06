using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Services;
using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess.Repositories;
using System.Data.Entity;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //public IUserRepository _repository = new UserRepository();
            //using (var db = new UserContext())
            //{

            //    User user = new User
            //    {
            //        Email = "aguy@gmail.com",
            //        Password = "123",
            //        DateOfBirth = "11/11/2011",
            //        City = "Long Beach",
            //        State = "CA",
            //        Country = "United States"
            //    };
            //    db.Users.Add(user);
            //    db.SaveChanges();

            //    // Display all Blogs from the database
            //    var query = from b in db.Users
            //                orderby b.Email
            //                select b;

            //    Console.WriteLine("All users in the database:");
            //    foreach (var item in query)
            //    {
            //        Console.WriteLine(item.Email);
            //    }

            //    Console.WriteLine("Press any key to exit...");
            //    Console.ReadKey();
            //}
        }
    }
}
