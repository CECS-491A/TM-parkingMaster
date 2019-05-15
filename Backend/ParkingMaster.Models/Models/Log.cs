using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
    public class Log
    {
        public string DateTime { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string IpAddress { get; set; }
        public string SessionId { get; set; }
    }
}
