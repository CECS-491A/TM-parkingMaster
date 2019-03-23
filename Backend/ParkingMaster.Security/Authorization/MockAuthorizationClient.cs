using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Security.Authorization.Contracts;

namespace ParkingMaster.Security.Authorization
{
    public class MockAuthorizationClient : IAuthorizationClient
    {
        /*
        private IClaimRepository _repository;
        public MockAuthorizationClient(IClaimRepository repository)
        {
            _repository = repository;
        }

        public Boolean Authorize(List<Claim> userClaims, Claim functionClaim)
        {

            // Check for null inputs
            if (functionClaim == null || userClaims == null)
            {
                return false;
            }

            // Check for null claims
            if (userClaims.Contains(null))
            {
                return false;
            }

            // Check if user has all claims to use the function
            if (userClaims.Contains(functionClaim))
            {
                return true;
            }

            if (!_repository.FunctionIsActive(functionClaim.Value))
            {
                return false;
            }

            // Check if user has a client claim
            string client = null;
            string user = null;
            userClaims.ForEach(delegate (Claim uClaim)
            {
                if (uClaim.Title == "Client")
                {
                    client = uClaim.Value;
                }
                if (uClaim.Title == "User")
                {
                    user = uClaim.Value;
                }
            });

            // If the user has a client, check if the client has authorization for the function
            if (client != null)
            {
                List<Claim> clientClaims = new List<Claim>();
                clientClaims = _repository.GetUserClaims(client);

                if (!clientClaims.Contains(functionClaim))
                {
                    return false;
                }
            }

            userClaims = _repository.GetUserClaims(user);

            // Check if user has all claims to use the function
            if (userClaims.Contains(functionClaim))
            {
                return true;
            }


            return false;
        }
        */
    }
}
