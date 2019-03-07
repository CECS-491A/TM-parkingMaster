using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess.Repositories
{
    public class ClaimRepository : Repository<EntityClaim>, IClaimRepository
    {
        public ClaimRepository() : base(new UserContext())
        {

        }

        public List<Claim> GetUserClaims(string owner)
        {
                throw new NotImplementedException();
        }

        public Boolean FunctionIsActive(string functionName)
        {
            throw new NotImplementedException();
        }
    }
}
