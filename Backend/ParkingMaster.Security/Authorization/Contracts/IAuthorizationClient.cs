using System;
using System.Collections.Generic;
using ParkingMaster.DataAccess.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Security.Authorization.Contracts
{
    public interface IAuthorizationClient
    {
        Boolean Authorize(List<Claim> userClaims, Claim functionClaims);
    }
}
