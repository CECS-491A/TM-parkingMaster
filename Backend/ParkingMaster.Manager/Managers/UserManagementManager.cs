using System;
using ParkingMaster.Models.DTO;
using ParkingMaster.Services.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Collections.Generic;
using ParkingMaster.Models.Constants;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Manager.Managers
{
    public class UserManagementManager
    {

        private UserManagementService _userManagementService;
        private SessionService _sessionService;
        private ClaimService _claimService;

        public UserManagementManager()
        {
            _userManagementService = new UserManagementService();
            _sessionService = new SessionService();
            _claimService = new ClaimService();
        }
        /*
        // I am considering moving context.SaveChanges() here, but maybe later
        public bool CreateUser(string email, string password, string dob, string city, string state, string country, string role, bool act)
        {
            try
            {
                User user = new User
                {
                    Email = email,
                    Password = password,
                    DateOfBirth = dob,
                    City = city,
                    State = state,
                    Country = country,
                    Role = role,
                    Activated = act
                };
                _userManagementService.CreateUser(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        */
        public ResponseDTO<bool> DeleteUser(Guid userId)
        {
            try
            {
                return _userManagementService.DeleteUser(userId);
            }
            catch (Exception e)
            {
                return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = "Failed to delete userID: " + userId + "\n" + e.Message
                };
            }
        }

        public async Task<HttpResponseMessage> DeleteUserFromApps(Guid id)
        {
            HttpClient client = new HttpClient();
            ISignatureService _signatureService = new SignatureService();
            // NEED A BETTER PLACE TO HOLD THIS... 
            string appID = "4850df59-831f-4b2c-a035-9de6ad324d76";

            ResponseDTO<UserAccountDTO> user = _userManagementService.GetUserByUserId(id);

            var payload = new Dictionary<string, string>();
            payload.Add("appId", appID);
            payload.Add("ssoUserId", user.Data.SsoId.ToString());
            payload.Add("email", user.Data.Username);
            payload.Add("timestamp", DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString());
            var signature = _signatureService.Sign(payload);
            payload.Add("signature", signature);
            var stringPayload = JsonConvert.SerializeObject(payload);
            var jsonPayload = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var request = await client.PostAsync("http://localhost:61348/api/users/appdeleteuser", jsonPayload);
            return request;
        }

        // Delete User From SsoRequest
        public ResponseDTO<HttpStatusCode> DeleteUser(SsoUserRequestDTO request)
        {
            ResponseDTO<HttpStatusCode> response = new ResponseDTO<HttpStatusCode>();
            ISignatureService _signatureService = new SignatureService();
            if (!_signatureService.isValidSignature(request.GetStringToSign(), request.Signature))
            {
                response.Data = (HttpStatusCode)400;
                response.Error = "Signature not valid";
                return response;
            }


            // Check if request id is in guid format
            Guid ssoId;
            try
            {
                ssoId = new Guid(request.SsoUserId);
            }
            catch (Exception e)
            {
                response.Data = (HttpStatusCode)400;
                response.Error = "SsoId provided was invalid";
                return response;
            }

            UserAccountDTO userAccount;
            ResponseDTO<UserAccountDTO> userAccountResponse = _userManagementService.GetUserBySsoId(ssoId);
            if (userAccountResponse.Data == null)
            {
                // TODO: Add a check if user did not exist or if it was a standard EntityFramework Error
                response.Data = (HttpStatusCode)404;
                response.Error = "Unable to find ssoId";
                return response;
            }
            else
            {
                userAccount = userAccountResponse.Data;
            }

            ResponseDTO<bool> boolResponse;
            try
            {
                boolResponse = _userManagementService.DeleteUser(userAccount.Id);
            }
            catch (Exception e)
            {
                response.Data = (HttpStatusCode)500;
                response.Error = "Failed to delete userID: " + userAccount.Id + "\n" + e.Message;
                return response;
            }

            if (boolResponse.Data)
            {
                response.Data = (HttpStatusCode)200;
                return response;
            }
            else
            {
                response.Data = (HttpStatusCode)500;
                response.Error = boolResponse.Error;
                return response;
            }
        }

        /*
        public bool UpdateUser(User user)
        {
            try
            {
                _userManagementService.UpdateUser(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userManagementService.GetAllUsers();
        }
    */
        public ResponseDTO<ParkingMasterFrontendDTO> SetRole(ParkingMasterFrontendDTO request)
        {
            ResponseDTO<ParkingMasterFrontendDTO> response = new ResponseDTO<ParkingMasterFrontendDTO>();

            // Check if request id is in guid format
            Guid sessionId;
            try
            {
                sessionId = new Guid(request.Token);
            }
            catch (Exception e)
            {
                response.Data = null;
                response.Error = ErrorStrings.REQUEST_FORMAT;
                return response;
            }

            // Check if role is allowed
            if (!Roles.IsFrontendSelectableRole(request.Role))
            {
                response.Data = null;
                response.Error = ErrorStrings.REQUEST_FORMAT;
                return response;
            }

            // Check if session is active and get user information
            ResponseDTO<Session> sessionDTO = _sessionService.GetSession(sessionId);

            // Only users with unassigned role are allowed to perform this action
            if (sessionDTO.Data.UserAccount.RoleType != Roles.UNASSIGNED)
            {
                response.Data = null;
                response.Error = ErrorStrings.UNAUTHORIZED_ACTION;
                return response;
            }

            // Set up user information that will change
            UserAccountDTO userAccountChanges = new UserAccountDTO()
            {
                Id = sessionDTO.Data.UserAccount.Id,
                RoleType = request.Role
            };

            // Make user changes
            ResponseDTO<UserAccountDTO> userAccountResponse = _userManagementService.SetRole(userAccountChanges);
            if (userAccountResponse.Data == null)
            {
                response.Data = null;
                response.Error = userAccountResponse.Error;
                return response;
            }
            List<Claim> newClaims = _claimService.GetLotManagerUserClaims(userAccountResponse.Data.Username).Data;

            ResponseDTO<bool> editClaimsResponse;
            try
            {
                editClaimsResponse = _userManagementService.ResetUserClaims(userAccountChanges.Id, newClaims);
            }
            catch(Exception e)
            {
                response.Data = null;
                response.Error = e.ToString();
                return response;
            }

            if (!editClaimsResponse.Data)
            {
                response.Data = null;
                response.Error = editClaimsResponse.Error;
                return response;
            }

            response.Data.Id = userAccountResponse.Data.Id.ToString();
            response.Data.Role = userAccountResponse.Data.RoleType;
            response.Data.Token = sessionId.ToString();

            return response;
        }

        // Delete User From SsoRequest
        public ResponseDTO<HttpStatusCode> LogoutUser(SsoUserRequestDTO request)
        {
            ResponseDTO<HttpStatusCode> response = new ResponseDTO<HttpStatusCode>();
            ISignatureService _signatureService = new SignatureService();

            if (!_signatureService.isValidSignature(request.GetStringToSign(), request.Signature))
            {
                response.Data = (HttpStatusCode)400;
                response.Error = "Signature not valid";
                return response;
            }

            // Check if request id is in guid format
            Guid ssoId;
            try
            {
                ssoId = new Guid(request.SsoUserId);
            }
            catch (Exception e)
            {
                response.Data = (HttpStatusCode)400;
                response.Error = "SsoId provided was invalid";
                return response;
            }

            UserAccountDTO userAccount;
            ResponseDTO<UserAccountDTO> userAccountResponse = _userManagementService.GetUserBySsoId(ssoId);
            if (userAccountResponse.Data == null)
            {
                // Returns a success because there are no sessions to delete
                // The user has never opend our app so do not stop the SSO logout from continuing
                response.Data = (HttpStatusCode)200;
                return response;
            }
            else
            {
                userAccount = userAccountResponse.Data;
            }

            ResponseDTO<bool> boolResponse;
            try
            {
                boolResponse = _sessionService.DeleteAllUserSessions(userAccount.Id);
            }
            catch (Exception e)
            {
                response.Data = (HttpStatusCode)500;
                response.Error = "Failed to delete sessions for userID: " + userAccount.Id + "\n" + e.Message;
                return response;
            }

            if (boolResponse.Data)
            {
                response.Data = (HttpStatusCode)200;
                return response;
            }
            else
            {
                response.Data = (HttpStatusCode)500;
                response.Error = boolResponse.Error;
                return response;
            }
        }
        
        public ResponseDTO<bool> LogoutUser(ParkingMasterFrontendDTO request)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();

            // Check if request id is in guid format
            Guid sessionId;
            try
            {
                sessionId = new Guid(request.Token);
            }
            catch (Exception e)
            {
                response.Data = false;
                response.Error = ErrorStrings.REQUEST_FORMAT;
                return response;
            }

            return _sessionService.DeleteSession(sessionId);
        }
    }
}