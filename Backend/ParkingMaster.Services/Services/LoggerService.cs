using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Services.Services;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Constants;
using Newtonsoft.Json;
using System.Net.Mail;

using System.IO;
using System.Net;

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

        public ResponseDTO<List<Log>> GetLogs()
        {
            try
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
                return new ResponseDTO<List<Log>>()
                {
                    Data = logs
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<Log>>()
                {
                    Data = null,
                    Error = "Failed to get logs"
                };
            }
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

            ResponseDTO<List<Log>> logList = GetLogs();
            logList.Data.Add(log);
            logWritten = WriteToLogFile(logList.Data);
            //TODO: If failed to write to log, increment failErrorLog counter
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

            ResponseDTO<List<Log>> logList = GetLogs();
            logList.Data.Add(log);
            logWritten = WriteToLogFile(logList.Data);

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

            ResponseDTO<List<Log>> logList = GetLogs();
            logList.Data.Add(log);
            logWritten = WriteToLogFile(logList.Data);
            //TODO: If failed to write to log, increment failTelemetryLog counter
            return logWritten;
        }

        public bool EmailAdmin()
        {
            MailMessage mailMessage = new MailMessage("fourmuskateers2018@gmail.com", "assoths@gmail.com");
            mailMessage.Subject = "This is a test email";
            mailMessage.Body = "This is a test email. Please reply if you receive it.";

            SmtpClient smtpClient = new SmtpClient();
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "fourmuskateers2018@gmail.com",
                Password = "a3h5j3l1"
            };
            //smtpClient.UseDefaultCredentials = false;
            smtpClient.Send(mailMessage);

            return true;
        }
    }
}
