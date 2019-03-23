using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Context
{
	public class AuthenticationContext : DbContext
	{
        public AuthenticationContext()
        {
            this.Database.Connection.ConnectionString = "Data Source=localhost;Initial Catalog=ParkingMaster;Integrated Security=True";
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<AuthenticationToken> AuthenticationTokens { get; set; }
	}

}
