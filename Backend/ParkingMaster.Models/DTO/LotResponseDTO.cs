using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Models.DTO
{
    public class LotResponseDTO
    {
        public List<Spot> SpotsList { get; set; }
        public byte[] LotMap { get; set; }
    }
}
