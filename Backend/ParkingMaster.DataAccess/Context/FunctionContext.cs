using ParkingMaster.Models.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess
{
    public class FunctionContext : DbContext
    {
        public FunctionContext()
        {
            this.Database.Connection.ConnectionString = "Data Source=localhost;Initial Catalog=ParkingMasterLocal;Integrated Security=True";
        }

        public DbSet<Function> Function { get; set; }
    }
}
