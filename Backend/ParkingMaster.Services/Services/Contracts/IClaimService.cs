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
        ResponseDTO<List<Claim>> GetStandardUserClaims();
        ResponseDTO<List<Claim>> GetStandardUserClaims(string username);
    }
}
