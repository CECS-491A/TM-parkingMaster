using System;
using System.Collections.Generic;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Services.Services
{
    public interface IClaimService
    {
        ResponseDTO<List<ClaimDTO>> GetAllUserClaims(string username);
        ResponseDTO<List<ClaimDTO>> GetAllUserClaims(Guid userId);
        ResponseDTO<List<Claim>> GetUnassignedUserClaims();
        ResponseDTO<List<Claim>> GetStandardUserClaims();
        ResponseDTO<List<Claim>> GetLotManagerUserClaims();
        ResponseDTO<List<Claim>> GetUserClaims(string role, string username);
    }
}
