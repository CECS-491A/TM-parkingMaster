using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingMaster.Models.Constants;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Manager.Managers
{
    public class ReservationManager : IReservationManager
    {
        private ReservationServices _reservationServices;
        private SessionService _sessionServices;
        private ILoggerService _loggerService;

        public ReservationManager()
        {
            _reservationServices = new ReservationServices();
            _sessionServices = new SessionService();
            _loggerService = new LoggerService();
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
                    Error = ErrorStrings.REQUEST_FORMAT
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
                    Error = ErrorStrings.REQUEST_FORMAT
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

            _loggerService.LogAction(LogConstants.ACTION_ADD_RESERVATION, sessionDTO.Data.Id.ToString(), sessionDTO.Data.SessionId.ToString());
            return _reservationServices.ReserveSpot(reservationDTO);
        }

        public ResponseDTO<UserSpotDTO> ExtendUserReservation(ReservationRequestDTO request)
        {
            // Check if all Guids are formatted properly
            Guid sessionId;
            try
            {
                sessionId = Guid.Parse(request.SessionId);
            }
            catch (Exception)
            {
                return new ResponseDTO<UserSpotDTO>()
                {
                    Data = null,
                    Error = ErrorStrings.REQUEST_FORMAT
                };
            }

            Guid spotId;
            try
            {
                spotId = Guid.Parse(request.SpotId);
            }
            catch (Exception)
            {
                return new ResponseDTO<UserSpotDTO>()
                {
                    Data = null,
                    Error = ErrorStrings.REQUEST_FORMAT
                };
            }

            ResponseDTO<Session> sessionDTO = _sessionServices.GetSession(sessionId);

            // If session data is null, then an error occured
            if (sessionDTO.Data == null)
            {
                return new ResponseDTO<UserSpotDTO>()
                {
                    Data = null,
                    Error = sessionDTO.Error

                };
            }

            ReservationDTO reservationDTO = new ReservationDTO()
            {
                DurationInMinutes = request.DurationInMinutes,
                UserId = sessionDTO.Data.UserAccount.Id,
                SpotId = spotId
            };
            _loggerService.LogAction(LogConstants.ACTION_EXTEND_RESERVATION, sessionDTO.Data.Id.ToString(), sessionDTO.Data.SessionId.ToString());
            return _reservationServices.ExtendReservation(reservationDTO);
        }

        public ResponseDTO<List<UserSpotDTO>> GetAllUserReservations(ParkingMasterFrontendDTO request)
        {
            // Check if all Guids are formatted properly
            Guid sessionId;
            try
            {
                sessionId = Guid.Parse(request.Token);
            }
            catch (Exception)
            {
                return new ResponseDTO<List<UserSpotDTO>>()
                {
                    Data = null,
                    Error = ErrorStrings.REQUEST_FORMAT
                };
            }

            ResponseDTO<Session> sessionDTO = _sessionServices.GetSession(sessionId);

            // If session data is null, then an error occured
            if (sessionDTO.Data == null)
            {
                return new ResponseDTO<List<UserSpotDTO>>()
                {
                    Data = null,
                    Error = sessionDTO.Error

                };
            }

            return _reservationServices.GetAllUserResverations(sessionDTO.Data.UserAccount.Id);
        }
    }
}