using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingMaster.Services.Services;
using ParkingMaster.Manager.Managers.Contracts;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.Constants;
using System.Security.Cryptography;
using System.Text;

namespace ParkingMaster.Manager.Managers
{
    public class LoginManager: ILoginManager
    {
        private ISessionService _sessionService;
        private IUserManagementService _userManagementService;
        private ISignatureService _signatureService;
        private IClaimService _claimService;
        private ILoggerService _loggerService;

        public LoginManager()
        {
            _sessionService = new SessionService();
            _userManagementService = new UserManagementService();
            _signatureService = new SignatureService();
            _claimService = new ClaimService();
            _loggerService = new LoggerService();
        }

        public ResponseDTO<Session> SsoLogin(SsoUserRequestDTO request)
        {
            ResponseDTO<Session> response = new ResponseDTO<Session>();
            
            // Before anything happens, validate that this request is coming from the known sso server
            if (!_signatureService.isValidSignature(request.GetStringToSign(), request.Signature))
            {
                response.Data = null;
                response.Error = "My signature: " + _signatureService.Sign(request.GetStringToSign()) + " Compared to: " + request.Signature;
                _loggerService.LogBadRequest(LogConstants.FAIL_LOGIN, request.SsoUserId, response.Error);
                return response;
            }

            // Protect against replay attacks by checking the timestamp
            if (DateTimeOffset.Now.AddSeconds(5).ToUnixTimeMilliseconds() < request.Timestamp)
            {
                response.Data = null;
                response.Error = ErrorStrings.OLD_SSO_REQUEST;
                _loggerService.LogBadRequest(LogConstants.FAIL_LOGIN, request.SsoUserId, response.Error);
                return response;
            }

            // Convert request SsoId into Guid
            Guid ssoId = new Guid(request.SsoUserId);

            // Search for user in database
            ResponseDTO<UserAccountDTO> userAccountResponse = _userManagementService.GetUserBySsoId(ssoId);
            UserAccountDTO userDTO = userAccountResponse.Data;

            // If the user does not exist in the data store, register the user as a standard user
            if (userAccountResponse.Data == null)
            {
                // Verify the email is not null
                if (request.Email == null)
                {
                    response.Data = null;
                    response.Error = "User email may not be null.";
                    _loggerService.LogBadRequest(LogConstants.FAIL_LOGIN, userDTO.SsoId.ToString(), response.Error);
                    return response;
                }

                // Create an unassigned user account
                UserAccount user = new UserAccount()
                {
                    SsoId = ssoId,
                    Username = request.Email,
                    IsActive = true,
                    IsFirstTimeUser = true,
                    RoleType = Roles.UNASSIGNED
                };
                List<Claim> newClaims = _claimService.GetUserClaims(Roles.UNASSIGNED, request.Email).Data;

                // Add user to datastore
                ResponseDTO<bool> createUserResponse = _userManagementService.CreateUser(user, newClaims);

                // Check if user creation succeded
                if (!createUserResponse.Data)
                {
                    response.Data = null;
                    response.Error = createUserResponse.Error;
                    _loggerService.LogBadRequest(LogConstants.FAIL_LOGIN, userDTO.SsoId.ToString(), response.Error);
                    return response;
                }

                // User now exists in database, proceed with login as normal
                userDTO = new UserAccountDTO(user);
            }

            // Create session for user
            ResponseDTO<Session> sessionResponseDTO = _sessionService.CreateSession(userDTO.Id);

            _loggerService.LogAction(LogConstants.ACTION_LOGIN, userDTO.SsoId.ToString(), sessionResponseDTO.Data.SessionId.ToString());

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