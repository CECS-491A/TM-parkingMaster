using ParkingMaster.Models.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess
{
	public class UserContext : DbContext
	{
        public UserContext()
        {
            this.Database.Connection.ConnectionString = "Data Source=localhost;Initial Catalog=ParkingMaster;Integrated Security=True";
        }

		public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<UserClaims> UserClaims { get; set; }
		public DbSet<PasswordSalt> PasswordSalts { get; set; }

	}
}
