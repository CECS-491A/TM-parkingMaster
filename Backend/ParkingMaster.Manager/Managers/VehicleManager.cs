//using ParkingMaster.DataAccess.Context;
using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Gateways;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ParkingMaster.Manager.Managers
{
    public class VehicleManager
    {
        private VehicleService _vehicleServices;
        private SessionService _sessionServices;

        public VehicleManager()
        {
            _vehicleServices = new VehicleService();
            _sessionServices = new SessionService();
        }

        public ResponseDTO<List<Vehicle>> GetAllUserVehicles(ParkingMasterFrontendDTO request)
        {
            // Check if sessionId is in Guid Format
            Guid sessionId;
            try
            {
                sessionId = Guid.Parse(request.Token);
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Vehicle>>()
                {
                    Data = null,
                    Error = "Token not a valid Guid"
                };
            }

            ResponseDTO<Session> sessionDTO = _sessionServices.GetSession(sessionId);

            // If session data is null, then an error occured
            if (sessionDTO.Data == null)
            {
                return new ResponseDTO<List<Vehicle>>()
                {
                    Data = null,
                    Error = sessionDTO.Error

                };
            }

            return _vehicleServices.GetAllUserVehicles(sessionDTO.Data.UserAccount.Id);
        }
    }
}