using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Security.Authorization.Contracts
{
    public interface IAuthorizationClient
    {

        ResponseDTO<Boolean> Authorize(string username, List<ClaimDTO> functionClaims);
        
    }
}
