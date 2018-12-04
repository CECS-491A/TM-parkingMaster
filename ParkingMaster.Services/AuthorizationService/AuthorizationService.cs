using System;
using System.Collections.Generic;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.Services
{
    public class AuthorizationService
    {

        public Boolean AuthorizeUserToFunction(List<Claim> requirementClaims, List<Claim> userClaims, List<Claim> bannedClaims)
        {
            // Initialize variables
            Boolean authorized = true;
            Claim blanketBan = new Claim("Ban All", "Ban All");

            // Check for null inputs
            if(requirementClaims == null || userClaims == null || bannedClaims == null)
            {
                return false;
            }

            // Check for null claims
            if(requirementClaims.Contains(null) || userClaims.Contains(null) || bannedClaims.Contains(null))
            {
                return false;
            }

            // Check if the functionality has been disabled for all users
            if (bannedClaims.Contains(blanketBan))
            {
                return false;
            }

            // Check if user has all claims to use the function
            requirementClaims.ForEach(delegate (Claim reqClaim)
            {
                if (!userClaims.Contains(reqClaim))
                {
                    authorized = false;
                }
            });

            // Check if user has been banned from the function
            bannedClaims.ForEach(delegate (Claim banClaim)
            {
                if (userClaims.Contains(banClaim))
                {
                    authorized = false;
                }
            });
            
            return authorized;
        }
    }
}
