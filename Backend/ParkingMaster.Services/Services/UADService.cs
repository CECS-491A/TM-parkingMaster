using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Services.Services;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.Constants;
using Newtonsoft.Json;

namespace ParkingMaster.Services.Services
{
    public class UADService
    {
        public List<Log> GetSuccessfulLogins(List<Log> logs)
        {
            var loginList = new List<Log>();
            foreach(var log in logs)
            {
                if(log.Action == LogConstants.ACTION_LOGIN)
                {
                    loginList.Add(log);
                }
            }
            return loginList;
        }

        public List<Log> GetFailedLogins(List<Log> logs)
        {
            var loginList = new List<Log>();
            foreach (var log in logs)
            {
                if (log.Action == LogConstants.FAIL_LOGIN)
                {
                    loginList.Add(log);
                }
            }
            return loginList;
        }
    }
}
