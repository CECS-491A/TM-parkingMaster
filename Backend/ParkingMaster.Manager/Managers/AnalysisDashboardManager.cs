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
    public class AnalysisDashboardManager
    {
        private UserManagementService _userManagementService;
        private SessionService _sessionService;
        private ILoggerService _loggerService;

        public AnalysisDashboardManager()
        {
            _userManagementService = new UserManagementService();
            _sessionService = new SessionService();
            _loggerService = new LoggerService();
        }

        public void SuccessVsFailLoginData()
        {

        }
    }
}