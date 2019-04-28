using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
    public class VehicleRequestDTO
    {
        public string SessionId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Plate { get; set; }
        public string State { get; set; }
        public string Vin { get; set; }
    }
}
