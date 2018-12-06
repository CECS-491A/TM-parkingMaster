using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess
{
    // This would be our mock database if I could get it to work.
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EntityClaim> Claims { get; set; }
    }
}
