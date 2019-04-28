using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
    public class ReservationRequestDTO
    {
        public string SessionId { get; set; }
        public string LotId { get; set; }
        public string SpotId { get; set; }
        public string VehicleVin { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
