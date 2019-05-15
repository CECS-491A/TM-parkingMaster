using System;
using System.Collections.Generic;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Constants;

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

        public ResponseDTO<List<Claim>> GetUnassignedUserClaims()
        {
            ResponseDTO<List<Claim>> response = new ResponseDTO<List<Claim>>();
            List<Claim> claimsList = new List<Claim>();

            claimsList.Add(new Claim("Action", "SetRole"));

            response.Data = claimsList;
            return response;
        }

        public ResponseDTO<List<Claim>> GetStandardUserClaims()
        {
            ResponseDTO<List<Claim>> response = new ResponseDTO<List<Claim>>();
            List<Claim> claimsList = new List<Claim>();

            claimsList.Add(new Claim("Action", "ReserveParkingSpot"));
            claimsList.Add(new Claim("Action", "UpdateReservation"));
            claimsList.Add(new Claim("Action", "ViewParkingLot"));
            claimsList.Add(new Claim("Action", "ViewAllParkingLots"));
            claimsList.Add(new Claim("Action", "AddVehicle"));

            response.Data = claimsList;
            return response;
        }

        public ResponseDTO<List<Claim>> GetLotManagerUserClaims()
        {
            ResponseDTO<List<Claim>> response = new ResponseDTO<List<Claim>>();
            List<Claim> claimsList = new List<Claim>();

            claimsList.Add(new Claim("Action", "AddParkingLot"));
            claimsList.Add(new Claim("Action", "DeleteParkingLot"));

            response.Data = claimsList;
            return response;
        }

        public ResponseDTO<List<Claim>> GetUserClaims(string role, string username)
        {
            ResponseDTO<List<Claim>> response;

            // Get base user claims
            switch (role)
            {
                case Roles.UNASSIGNED:
                    response = GetUnassignedUserClaims();
                    break;

                case Roles.STANDARD:
                    response = GetStandardUserClaims();
                    break;

                case Roles.LOTMANAGER:
                    response = GetLotManagerUserClaims();
                    break;

                default:
                    return new ResponseDTO<List<Claim>>()
                    {
                        Data = null,
                        Error = "Invalid roletype."
                    };
            }

            // Add user specific claims
            response.Data.Add(new Claim("User", username));

            return response;
        }

    }
}
