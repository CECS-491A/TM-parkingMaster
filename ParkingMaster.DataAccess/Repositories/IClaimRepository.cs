using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess.Repositories
{
    public interface IClaimRepository : IRepository<EntityClaim>
    {
        List<Claim> GetUserClaims(string owner);
        Boolean FunctionIsActive(string function);
    }
}
