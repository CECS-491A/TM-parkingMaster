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
    public class TermsOfServiceManager
    {
        TermsOfServiceService _tosService;
        SessionService _sessionService;
        UserManagementService _userManagementService;

        public TermsOfServiceManager()
        {
            _tosService = new TermsOfServiceService();
            _sessionService = new SessionService();
            _userManagementService = new UserManagementService();
        }

        public ResponseDTO<TermsOfService> GetActiveTOS(ParkingMasterFrontendDTO request)
        {
            ResponseDTO<TermsOfService> response = new ResponseDTO<TermsOfService>();

            // Convert token into Guid
            Guid tokenGuid;
            try
            {
                tokenGuid = new Guid(request.Token);
            }
            catch
            {
                response.Data = null;
                response.Error = ErrorStrings.REQUEST_FORMAT;
                return response;
            }

            // Check session in data store
            ResponseDTO<Session> sessionResponseDTO = _sessionService.GetSession(tokenGuid);

            // If session is not found, return error
            if (sessionResponseDTO.Data == null)
            {
                response.Data = null;
                response.Error = sessionResponseDTO.Error;
                return response;
            }

            // Check if user is currently disabled
            if (!sessionResponseDTO.Data.UserAccount.IsActive)
            {
                response.Data = null;
                response.Error = ErrorStrings.USER_DISABLED;
                return response;
            }

            return _tosService.GetActiveTermsOfService();
        }

        public ResponseDTO<bool> UserAcceptTOS(ParkingMasterFrontendDTO request)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();

            // Convert token into Guid
            Guid tokenGuid;
            try
            {
                tokenGuid = new Guid(request.Token);
            }
            catch
            {
                response.Data = false;
                response.Error = ErrorStrings.REQUEST_FORMAT;
                return response;
            }

            // Check session in data store
            ResponseDTO<Session> sessionResponseDTO = _sessionService.GetSession(tokenGuid);

            // If session is not found, return error
            if (sessionResponseDTO.Data == null)
            {
                response.Data = false;
                response.Error = sessionResponseDTO.Error;
                return response;
            }

            // Check if user is currently disabled
            if (!sessionResponseDTO.Data.UserAccount.IsActive)
            {
                response.Data = false;
                response.Error = ErrorStrings.USER_DISABLED;
                return response;
            }

            return _userManagementService.AcceptTOS(sessionResponseDTO.Data.UserAccount.Id);
        }
    }
}