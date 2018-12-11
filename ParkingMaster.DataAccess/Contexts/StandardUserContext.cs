using System.Data.Entity; 

namespace ParkingMaster.DataAccess
{
    //
    public class StandardUserContext : DbContext
    {
        public DbSet<StandardUser> StandardUser { get; set; }
        public StandardUserContext() : base("ParkingMaster") { }
    }
}
