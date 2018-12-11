using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Function> Functions { get; set; }

        public DatabaseContext() : base("ParkingMaster") {
            Database.SetInitializer(new DatabaseInitializer()); // currently DropCreateDatabaseAlways for testing!!!
        }

    }
}
