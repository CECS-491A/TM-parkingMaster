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

        public ResponseDTO<bool> StoreVehicle(VehicleRequestDTO request)
        {
            // Check if sessionId is in Guid Format
            Guid sessionId;
            try
            {
                sessionId = Guid.Parse(request.SessionId);
            }
            catch (Exception)
            {
                return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = "SessionId is not a valid Guid"
                };
            }

            ResponseDTO<Session> sessionDTO = _sessionServices.GetSession(sessionId);

            // If session data is null, then an error occured
            if (sessionDTO.Data == null)
            {
                return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = sessionDTO.Error

                };
            }

            // Input Validation
            if(request.Make == null || request.Model == null || request.Year == null || request.Plate == null || request.State == null || request.Vin == null)
            {
                return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = "Vehicle object not fully defined."

                };
            }

            // Check if year is an integer
            int year;
            if(!int.TryParse(request.Year, out year))
            {
                return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = "Year is not an integer."

                };
            }

            // Create the Vehicle to be stored
            Vehicle userVehicle = new Vehicle()
            {
                OwnerId = sessionDTO.Data.UserAccount.Id,
                Make = request.Make,
                Model = request.Model,
                Year = year,
                Plate = request.Plate,
                State = request.State,
                Vin = request.Vin
            };
            return _vehicleServices.StoreVehicle(userVehicle);
        }
    }
}