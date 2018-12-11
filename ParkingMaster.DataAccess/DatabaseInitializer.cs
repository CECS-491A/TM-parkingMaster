using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>//CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            IList<User> UserSeed = new List<User>
            {
                new User
                {
                    Email = "pnguyen@gmail.com",
                    Password = "123456",
                    DateOfBirth = "09/13/1999",
                    City = "Sacramento",
                    State = "CA",
                    Country = "US",
                    Role = "Standard",
                    UserClaims={
                        new Claim("User", "pnguyen@gmail.com"),
                        new Claim("Action", "DisabledAction"),
                        new Claim("Action", "CreateOtherUser"),
                        new Claim("Action", "Logout"),
                        new Claim("Action", "Client2Action"),
                        new Claim("Client", "client1@yahoo.com")
                    },
                    Activated = false
                },
                new User
                {
                    Email = "plaurent@yahoo.com",
                    Password = "mountain",
                    DateOfBirth = "11/03/1996",
                    City = "Trenton",
                    State = "NJ",
                    Country = "US",
                    Role = "Standard",
                    UserClaims={
                        new Claim("User", "plaurent@yahoo.com"),
                        new Claim("Action", "DisabledAction"),
                        new Claim("Action", "CreateOtherUser"),
                        new Claim("Action", "Logout"),
                        new Claim("Client", "client1@yahoo.com")
                    },
                    Activated = false
                },
                new User
                {
                    Email = "tnguyen@gmail.com",
                    Password = "123456",
                    DateOfBirth = "12/25/1950",
                    City = "Albany",
                    State = "NY",
                    Country = "US",
                    Role = "Standard",
                    UserClaims={
                        new Claim("User", "tnguyen@gmail.com"),
                        new Claim("Action", "Logout"),
                        new Claim("Action", "Client2Action"),
                        new Claim("Client", "client2@yahoo.com")
                    },
                    Activated = false
                }
            };
            context.Users.AddRange(UserSeed);
            base.Seed(context);
        }

    }
}
