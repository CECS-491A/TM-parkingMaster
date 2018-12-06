using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess.Repositories
{
    class ClaimRepository : Repository<EntityClaim>, IClaimRepository
    {
        public ClaimRepository() : base(new UserContext())
        {

        }
    }
}
