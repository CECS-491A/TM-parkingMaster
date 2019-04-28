using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Services.Services
{
    public class VehicleService
    {
        private VehicleGateway _vehicleGateway;

        public VehicleService()
        {
            _vehicleGateway = new VehicleGateway();
        }

        public ResponseDTO<List<Vehicle>> GetAllUserVehicles(Guid userId)
        {
            return _vehicleGateway.GetAllUserVehicles(userId);
        }

        public ResponseDTO<bool> StoreVehicle(Vehicle vehicle)
        {
            return _vehicleGateway.StoreVehicle(vehicle);
        }
    }
}
