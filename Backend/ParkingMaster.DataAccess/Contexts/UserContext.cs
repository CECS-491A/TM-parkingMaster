using ParkingMaster.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess
{
    // User management Tables
    public class UserContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        // whatever other tables we have in the DB
        public UserContext() : base("ParkingMaster") { }
    }
}
