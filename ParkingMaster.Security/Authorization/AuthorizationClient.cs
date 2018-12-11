using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Security.Authorization.Contracts;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.Security.Authorization
{
    public class AuthorizationClient : IAuthorizationClient
    {

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


            // TODO: Check if function is disabled
            //if (!GetIsFunctionActive(functionClaim.value))
            //{
            //    return false;
            //}

            // Check if user has a client claim
            string client = null;
            string user = null;
            userClaims.ForEach(delegate(Claim uClaim)
            {
                if (uClaim.Title == "Client")
                {
                    client = uClaim.Value;
                }
                if(uClaim.Title == "Username")
                {
                    user = uClaim.Value;
                }
            });

            // Cannot authenticate a user that does not exist
            if(user == null)
            {
                return false;
            }

            // If the user has a client, check if the client has authorization for the function
            if(client != null)
            {
                List<Claim> clientClaims = new List<Claim>();
                // TODO: Read client claims in persistence
                //clientClaims = getUserClaims(client);

                if (!clientClaims.Contains(functionClaim))
                {
                    return false;
                }
            }

            // TODO: Read user claims persistence
            //userClaims = getUserClaims(user);

            // Check if user has all claims to use the function
            if (userClaims.Contains(functionClaim))
            {
                return true;
            }


            return false;
        }

    }
}
