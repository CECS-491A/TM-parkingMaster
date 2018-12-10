using System.Data.Entity; 

namespace ParkingMaster.DataAccess
{
    //
    public class IndividualProfileContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public IndividualProfileContext() : base("GetUsGrub") { }
    }
}
