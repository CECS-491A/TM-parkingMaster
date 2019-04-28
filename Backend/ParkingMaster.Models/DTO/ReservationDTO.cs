using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
    public class ReservationDTO
    {
        public Guid SpotId { get; set; }
        public Guid UserId { get; set; }
        public string VehicleVin { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
