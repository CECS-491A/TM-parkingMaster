using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess;
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
            using (var ctx = new DatabaseContext())
            {
                //var user = new User()
                //{
                //    Email = "tnguyen@gmail.com",
                //    Password = "totallyhashedpw",
                //    DateOfBirth = "12/25/1996",
                //    City = "Carson",
                //    State = "CA",
                //    Country = "US",
                //    Role = "Standard",
                //    Activated = false
                //};

                //ctx.Users.Add(user);
                //ctx.SaveChanges();

                var query = from u in ctx.Users orderby u.Email select u;
                foreach (var item in query)
                {
                    Console.WriteLine(item.Email);
                }

                Console.ReadKey();

            }
        }
    }
}
