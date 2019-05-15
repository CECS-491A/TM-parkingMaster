using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingMaster.Models.Constants;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Manager.Managers
{
    public class LogManager
    {
        private UserManagementService _userManagementService;
        private SessionService _sessionService;
        private ILoggerService _loggerService;

        public LogManager()
        {
            _userManagementService = new UserManagementService();
            _sessionService = new SessionService();
            _loggerService = new LoggerService();
        }

        public ResponseDTO<List<Log>> GetAllLogs(ParkingMasterFrontendDTO request)
        {
            // Check if sessionId is in Guid Format
            Guid sessionId;
            try
            {
                sessionId = Guid.Parse(request.Token);
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Log>>()
                {
                    Data = null,
                    Error = "Token not a valid Guid"
                };
            }

            ResponseDTO<Session> sessionDTO = _sessionService.GetSession(sessionId);

            // If session data is null, then an error occured
            if (sessionDTO.Data == null)
            {
                return new ResponseDTO<List<Log>>()
                {
                    Data = null,
                    Error = sessionDTO.Error

                };
            }

            return _loggerService.GetLogs();
        }
    }
}