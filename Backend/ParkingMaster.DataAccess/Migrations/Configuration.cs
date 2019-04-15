namespace ParkingMaster.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ParkingMaster.Models.Models;
    using ParkingMaster.Models.DTO;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using ParkingMaster.DataAccess.Gateways;

    internal sealed class Configuration : DbMigrationsConfiguration<ParkingMaster.DataAccess.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ParkingMaster.DataAccess.UserContext context)
        {

            var userGateway = new UserGateway(context);
            userGateway.ResetDatabase();

            UserAccount user = new UserAccount()
            {
                SsoId = new Guid("12345678-1234-1234-1234-123456789ABC"),
                Username = "pnguyen@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            // Creating a list of claims
            List<Claim> claims = new List<Claim>
            {
                new Claim("User", "pnguyen@gmail.com"),
                new Claim("Action", "DisabledAction"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout"),
                new Claim("Parent", "client1@yahoo.com")
            };
            userGateway.StoreIndividualUser(user, claims);


            user = new UserAccount()
            {
                SsoId = new Guid("87654321-4321-4321-4321-CBA987654321"),
                Id = new Guid("88888888-4444-3333-2222-111111111111"),
                Username = "plaurent@yahoo.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            claims = new List<Claim>
            {
                new Claim("User", "plaurent@yahoo.com"),
                new Claim("Action", "DisabledAction"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout"),
                new Claim("Parent", "client1@yahoo.com")
            };
            userGateway.StoreIndividualUser(user, claims);


            user = new UserAccount()
            {
                SsoId = new Guid("24682468-2468-2468-2468-CBA987654321"),
                Username = "tnguyen@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            claims = new List<Claim>
            {
                new Claim("User", "tnguyen@gmail.com"),
                new Claim("Action", "Logout"),
                new Claim("Action", "Client2Action"),
                new Claim("Parent", "client2@gmail.com")
            };
            userGateway.StoreIndividualUser(user, claims);


            user = new UserAccount()
            {
                SsoId = new Guid("13571357-1357-1357-1357-CBA987654321"),
                Username = "client1@yahoo.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "lotmanager"
            };
            claims = new List<Claim>
            {
                new Claim("User", "client1@yahoo.com"),
                new Claim("Action", "Client1Action"),
                new Claim("Action", "DisabledAction"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout")
            };
            userGateway.StoreIndividualUser(user, claims);


            user = new UserAccount()
            {
                SsoId = new Guid("13571357-1357-1357-1357-CBA987654321"),
                Username = "client2@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "lotmanager"
            };
            claims = new List<Claim>
            {
                new Claim("User", "client2@gmail.com"),
                new Claim("Action", "Client2Action"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout"),
                new Claim("Action", "Client2Action")
            };
            userGateway.StoreIndividualUser(user, claims);

            var user1 = new UserAccount()
            {
                SsoId = new Guid("2AE9A868-17AA-490F-9094-5907D2E64EBB"),
                Username = "user1@yahoo.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            claims = new List<Claim>
            {
                new Claim("User", "user1@yahoo.com"),
                new Claim("Action", "DisabledAction"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout"),
                new Claim("Parent", "client1@yahoo.com")
            };
            userGateway.StoreIndividualUser(user1, claims);


            var functionGateway = new FunctionGateway(context);
            functionGateway.ResetDatabase();

            functionGateway.StoreFunction(new Function("DisableAction", false));
            functionGateway.StoreFunction(new Function("CreateOtherUser", true));
            functionGateway.StoreFunction(new Function("Logout", true));
            functionGateway.StoreFunction(new Function("Client1Action", true));
            functionGateway.StoreFunction(new Function("Client2Action", true));

            var sessionGateway = new SessionGateway(context);
            sessionGateway.ResetDatabase();

            sessionGateway.StoreSession(new Session(user1.Id));

            var lotGateway = new LotGateway(context);
            lotGateway.ResetDatabase();

            UserAccountDTO testUser = userGateway.GetUserByUsername("pnguyen@gmail.com").Data;
            Lot testLot = new Lot
            {
                LotId = new Guid("205296C2-9827-404B-9C95-2A28E00BFE0A"),
                OwnerId = testUser.Id,
                LotName = "TestLot",
                Address = "123 Testing St.",
                Cost = 20.0,
                UserAccount = testUser
            };
            List<Spot> testSpots = new List<Spot>
            {
                new Spot
                {
                    SpotId = new Guid("ABADF4D9-7310-4E1B-9A5C-66E0055DD99D"),
                    SpotName = "1",
                    LotId = testLot.LotId,
                    LotName = testLot.LotName,
                    IsHandicappedAccessible = false,
                    IsTaken = false,
                    Lot = testLot
                },
                new Spot
                {
                    SpotId = new Guid("D1B84BA3-21EE-4E44-A415-606E2F6F8FAA"),
                    SpotName = "2",
                    LotId = testLot.LotId,
                    LotName = testLot.LotName,
                    IsHandicappedAccessible = false,
                    IsTaken = false,
                    Lot = testLot
                }
            };

            lotGateway.AddLot(testLot, testSpots);
        }
    }
}
