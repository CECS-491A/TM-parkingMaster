using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingMaster.Services.Services;
using ParkingMaster.Manager.Managers.Contracts;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using System.Security.Cryptography;
using System.Text;

namespace ParkingMaster.Manager.Managers
{
    public class LoginManager: ILoginManager
    {
        private SessionService _sessionService;
        private UserManagementService _userManagementService;
        // IMPORTANT: For development, remove before deployment
        private string sharedSecretKey = "8ED06CB19DF0FF3654722F5ED6D8878909F8A456C7DA8A85E6D72FBAAB16CDCF";

        public LoginManager()
        {
            _sessionService = new SessionService();
            _userManagementService = new UserManagementService();
        }

        public ResponseDTO<Session> SsoLogin(LoginRequestDTO request)
        {

            // Check the reuqest signature
            string stringToSign = request.GetStringToSign();
            HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.ASCII.GetBytes(sharedSecretKey));
            byte[] launchPayloadBuffer = Encoding.ASCII.GetBytes(stringToSign);
            byte[] signatureBytes = hmacsha256.ComputeHash(launchPayloadBuffer);
            string signature = Convert.ToBase64String(signatureBytes);

            // If request signature does not match our signature, do not authenticate user
            if(signature != request.Signature)
            {
                return new ResponseDTO<Session>()
                {
                    Data = null,
                    Error = stringToSign + " Produced the Invalid Signature: " + signature
                };
            }

            // Convert request SsoId into Guid
            Guid SsoId = new Guid(request.SsoId);

            // Search for user in database
            ResponseDTO<UserAccountDTO> userAccountResponse = _userManagementService.GetUserBySsoId(SsoId);
            if(userAccountResponse.Data == null)
            {
                // TODO: Should be user registration
                //       Currently just doesnt authorize
                return new ResponseDTO<Session>()
                {
                    Data = null,
                    Error = "User doesn't exist"
                };
            }

            // Create session for user
            ResponseDTO<Session> sessionResponseDTO = _sessionService.CreateSession(userAccountResponse.Data.Id);

            return sessionResponseDTO;
        }
    }
}