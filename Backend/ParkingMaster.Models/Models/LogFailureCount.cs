using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
    public class LogFailureCount
    {
        public string LogType { get; set; }
        public int FailCount { get; set; }

        public bool IncrementErrorFailCount()
        {
            try
            {
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool IncrementTelemetryFailCount()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EmailLogFailure()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
