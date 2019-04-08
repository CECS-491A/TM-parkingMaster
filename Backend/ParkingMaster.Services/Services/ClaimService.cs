using System;
using System.Collections.Generic;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Services.Services
{
    public class ClaimService: IClaimService
    {
        private UserGateway _userGateway;

        public ClaimService(UserGateway userGateway)
        {
            _userGateway = userGateway;
        }

        public ClaimService()
        {
            _userGateway = new UserGateway();
        }

        public ResponseDTO<List<ClaimDTO>> GetAllUserClaims(string username)
        {
            ResponseDTO<List<ClaimDTO>> response = new ResponseDTO<List<ClaimDTO>>();
            response = _userGateway.GetUserClaims(username);

            return response;
        }

        public ResponseDTO<List<ClaimDTO>> GetAllUserClaims(Guid userId)
        {
            ResponseDTO<List<ClaimDTO>> response = new ResponseDTO<List<ClaimDTO>>();
            response = _userGateway.GetUserClaims(userId);

            return response;
        }

        public ResponseDTO<List<Claim>> GetStandardUserClaims()
        {
            ResponseDTO<List<Claim>> response = new ResponseDTO<List<Claim>>();
            List<Claim> claimsList = new List<Claim>();

            claimsList.Add(new Claim("Action", "ReserveParkingSpot"));
            claimsList.Add(new Claim("Action", "UpdateParkingSpot"));
            claimsList.Add(new Claim("Action", "ViewParkingLots"));

            response.Data = claimsList;
            return response;
        }

        public ResponseDTO<List<Claim>> GetStandardUserClaims(string username)
        {
            // Get base standard user claims
            ResponseDTO<List<Claim>> response = GetStandardUserClaims();

            // Add user specific claims
            response.Data.Add(new Claim("User", username));

            return response;
        }

    }
}
