using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Manager.Managers
{
    public class ReservationManager : IReservationManager
    {
        private ReservationServices _reservationServices;
        private SessionService _sessionServices;

        public ReservationManager()
        {
            _reservationServices = new ReservationServices();
            _sessionServices = new SessionService();
        }

        public ResponseDTO<bool> ReserveSpot(ReservationRequestDTO request)
        {
            // Check if all Guids are formatted properly
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

            Guid spotId;
            try
            {
                spotId = Guid.Parse(request.SpotId);
            }
            catch (Exception)
            {
                return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = "SpotId is not a valid Guid"
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

            // TODO: Check if vehicle exists in data store

            ReservationDTO reservationDTO = new ReservationDTO()
            {
                DurationInMinutes = request.DurationInMinutes,
                UserId = sessionDTO.Data.UserAccount.Id,
                VehicleVin = request.VehicleVin,
                SpotId = spotId
            };

            return _reservationServices.ReserveSpot(reservationDTO);
        }
    }
}