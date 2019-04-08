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
        public DbSet<Function> Function { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Spot> Spots { get; set; }
    }
}
