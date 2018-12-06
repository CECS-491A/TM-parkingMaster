using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess
{
    // This is basically our mock database.
    class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
