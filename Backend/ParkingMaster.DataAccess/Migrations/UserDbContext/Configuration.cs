using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
	class Configuration : DbMigrationsConfiguration<ParkingMaster.DataAccess.UserContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			MigrationsDirectory = @"Migrations\UserDbContext";
		}

		//Seed method
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

            var functionGateway = new FunctionGateway(context);
            functionGateway.ResetDatabase();

            functionGateway.StoreFunction(new Function("DisableAction", false));
            functionGateway.StoreFunction(new Function("CreateOtherUser", true));
            functionGateway.StoreFunction(new Function("Logout", true));
            functionGateway.StoreFunction(new Function("Client1Action", true));
            functionGateway.StoreFunction(new Function("Client2Action", true));

        }
    }
}
