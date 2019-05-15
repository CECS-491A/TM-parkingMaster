using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Services.Services;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.Constants;
using Newtonsoft.Json;

using System.IO;

namespace ParkingMaster.Services.Services
{
    public class LoggerService : ILoggerService
    {

        public bool CreateLog(string fileName, string path)
        {
            //TODO: File currently hardpathed, currently does not handle new log file creation due to
            // max memory capacity or missing
            return true;
        }
        
        public bool WriteToLogFile(List<Log> logs)
        {
            try
            {
                // Creates file if doesnt exist, otherwise it writes over it
                using(StreamWriter file = File.CreateText(LogConstants.PATH_LOG_CURRENT))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, logs);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public List<Log> GetLogs()
        {
            var logs = new List<Log>();
            if (new FileInfo(LogConstants.PATH_LOG_CURRENT).Length != 0)
            {
                using (StreamReader sr = new StreamReader(LogConstants.PATH_LOG_CURRENT))
                {
                    var file = sr.ReadToEnd();
                    logs = JsonConvert.DeserializeObject<List<Log>>(file);
                }
            }
            return logs;
        }

        public List<Log> GetLogs(string month, string year)
        {
            var logs = new List<Log>();
            return logs;
        }

        public bool LogError(string action, string userId, string errorSite, string errorMsg, string sessId)
        {
            var logWritten = false;

            var log = new Log
            {
                Action = action,
                User = userId,
                DateTime = DateTime.UtcNow.ToString(),
                Description = errorMsg,
                SessionId = sessId
            };

            var logList = GetLogs();
            logList.Add(log);

            logWritten = WriteToLogFile(logList);

            return logWritten;
            
        }

        public bool LogBadRequest(string action, string userId, string errorMsg)
        {
            var logWritten = false;

            var log = new Log
            {
                Action = action,
                User = userId,
                DateTime = DateTime.UtcNow.ToString(),
                Description = action + " occured. " + errorMsg // FailLogin occured
            };

            var logList = GetLogs();
            logList.Add(log);

            logWritten = WriteToLogFile(logList);

            return logWritten;
        }

        public bool LogAction(string action, string userId, string sessId)
        {
            var logWritten = false;

            var log = new Log
            {
                Action = action,
                User = userId,
                DateTime = DateTime.UtcNow.ToString(),
                Description = action + " by " + userId,
                SessionId = sessId
            };

            var logList = GetLogs();
            logList.Add(log);

            logWritten = WriteToLogFile(logList);

            return logWritten;
        }
    }
}
