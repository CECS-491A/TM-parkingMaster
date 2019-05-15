using System;
using System.Collections.Generic;
using ParkingMaster.Models.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Services.Services
{
    public interface ILoggerService
    {
        bool CreateLog(string fileName, string path);
        bool WriteToLogFile(List<Log> logs);
        List<Log> GetLogs();
        List<Log> GetLogs(string month, string year);
        bool LogError(string action, string userId, string errorSite, string errorMsg, string sessId);
        bool LogBadRequest(string action, string userId, string errorMsg);
        bool LogAction(string action, string userId, string sessId);

    }
}
