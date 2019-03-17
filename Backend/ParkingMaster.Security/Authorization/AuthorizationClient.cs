using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Security.Authorization.Contracts;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Security.Authorization
{
    public class AuthorizationClient : IAuthorizationClient
    {

        private UserGateway userGateway;

        public AuthorizationClient()
        {
            userGateway = new UserGateway();
        }

        public ResponseDTO<Boolean> Authorize(string username, List<ClaimDTO> functionClaims)
        {
            ResponseDTO<Boolean> response = new ResponseDTO<bool>();
            // Check for null inputs
            if (functionClaims == null || username == null)
            {
                response.Data = false;
                response.Error = "Input was Null";
                return response;
            }

            // TODO: Check if function is active


            // Authorize user
            response = AuthorizeUser(username, functionClaims);

            return response;
        }

        private ResponseDTO<Boolean> AuthorizeUser(string username, List<ClaimDTO> functionClaims)
        {
            List<ClaimDTO> userClaims;
            ResponseDTO<Boolean> response = new ResponseDTO<Boolean>();
            ResponseDTO<List<ClaimDTO>> claimResponse = userGateway.GetUserClaims(username);

            // If error occured retrieving user claims return unauthorized with error message
            if(claimResponse == null)
            {
                response.Data = false;
                response.Error = "Failed to read claims of user: " + username;
                return response;
            }
            // place user claims in a new object for better code readability
            else
            {
                userClaims = claimResponse.Data;
            }

            // Check if user has a parent claim
            // The existance of a parent claim indicates that the current user authorization depends on the parent user
            string parent = null;
            userClaims.ForEach(delegate (ClaimDTO uClaim)
            {
                if (uClaim.Title == "Parent")
                {
                    parent = uClaim.Value;
                }
            });

            // If the user has a parent user, recursively authorize the parent user
            if (parent != null)
            {
                response = AuthorizeUser(parent, functionClaims);

                // If parent user is not authorized to use this function do not authorize the current user
                if (!response.Data)
                {
                    return response;
                }
            }

            // Check if user has all claims to use the function
            foreach(ClaimDTO claim in functionClaims)
            if (userClaims.Contains(claim))
            {
                response.Data = true;
                return response;
            }

            // Default to an unauthorized user
            response.Data = false;
            return response;
        }

    }
}
