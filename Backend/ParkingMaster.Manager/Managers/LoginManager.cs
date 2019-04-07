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
        private ISessionService _sessionService;
        private IUserManagementService _userManagementService;
        private ITokenService tokenService = new TokenService();

        public LoginManager()
        {
            _sessionService = new SessionService();
            _userManagementService = new UserManagementService();
        }

        public ResponseDTO<Session> SsoLogin(SsoUserRequestDTO request)
        {
            ResponseDTO<Session> response = new ResponseDTO<Session>();
            
            // Before anything happens, validate that this request is coming from the known sso server
            if (!tokenService.isValidSignature(request.GetStringToSign(), request.Signature))
            {
                response.Data = null;
                response.Error = "Signature not valid";
                return response;
            }

            // Convert request SsoId into Guid
            Guid SsoId = new Guid(request.SsoUserId);

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

        public ResponseDTO<ParkingMasterFrontendDTO> SessionChecker(string sessionId)
        {
            ResponseDTO<ParkingMasterFrontendDTO> response = new ResponseDTO<ParkingMasterFrontendDTO>();
            
            // Convert token into Guid
            Guid tokenGuid;
            try
            {
                tokenGuid = new Guid(sessionId);
            }
            catch
            {
                response.Data = null;
                response.Error = "Invalid Token: " + sessionId;
                return response;
            }


            

            // Check session in data store
            ResponseDTO<Session> sessionResponseDTO = _sessionService.GetSession(tokenGuid);

            // If session is not found, return error
            if(sessionResponseDTO.Data == null)
            {
                response.Data = null;
                response.Error = sessionResponseDTO.Error;
                return response;
            }

            // Get user data from data store
            ResponseDTO<UserAccountDTO> userResponseDTO = _userManagementService.GetUserByUserId(sessionResponseDTO.Data.Id);
            if(userResponseDTO.Data == null)
            {
                response.Data = null;
                response.Error = userResponseDTO.Error;
                return response;
            }

            response.Data = new ParkingMasterFrontendDTO();
            // Return response info
            response.Data.Id = userResponseDTO.Data.Id.ToString();
            response.Data.Username = userResponseDTO.Data.Username;
            response.Data.Role = userResponseDTO.Data.RoleType;
            response.Data.Token = sessionResponseDTO.Data.SessionId.ToString();
            return response;
        }
    }
}