using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Models.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess.Context
{
    public class LotContext : DbContext
    {
        public LotContext()
        {
            this.Database.Connection.ConnectionString = "Data Source=localhost;Initial Catalog=ParkingMaster;Integrated Security=True";
        }

        public DbSet<Lot> Lots { get; set; }
        public DbSet<Spot> Spots { get; set; }

    }
}
