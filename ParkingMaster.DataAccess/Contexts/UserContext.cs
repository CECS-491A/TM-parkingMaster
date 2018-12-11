using ParkingMaster.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess
{
    // User management Tables
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<UserClaims> UserClaims { get; set; }
        
        // whatever other tables we have in the DB
        public UserContext() : base("ParkingMaster") { }
    }
}
