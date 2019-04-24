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
    public class LotManagementManager
    {
        private ILotManagementService _lotManagementService;
        private ISessionService _sessionService;
        private readonly LotGateway _lotGateway;
        private readonly UserGateway _userGateway;
        private readonly SessionService _sessionServices;

        public LotManagementManager()
        {
            _lotGateway = new LotGateway();
            _userGateway = new UserGateway();
            _lotManagementService = new LotManagementService(_lotGateway, _userGateway);
            _sessionServices = new SessionService();
        }

        public ResponseDTO<Boolean> AddLot(HttpRequest httprequest)//(Guid ownerid, string lotname, string address, double cost, HttpPostedFile file)
        {
            //ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(httpRequest["token"]);
            try
            {
                var token = httprequest["token"];
                ResponseDTO<Session> SessionDTO = _sessionService.GetSession(new Guid(token));
                if (SessionDTO.Data != null)
                {
                    var ownerid = SessionDTO.Data.UserAccount.Id;
                    //var username = httprequest["username"];
                    var lotname = httprequest["lotname"];
                    var address = httprequest["address"];
                    var cost = Convert.ToDouble(httprequest["cost"]);
                    var useraccount = SessionDTO.Data.UserAccount;
                    var spotfile = httprequest.Files["file"];
                    var spotmap = httprequest.Files["map"];
                    ResponseDTO<Boolean> response = _lotManagementService.AddLot(ownerid, lotname, address, cost, useraccount, spotfile);
                    return response;
                }
                else
                {
                    return new ResponseDTO<Boolean>()
                    {
                        Data = false,
                        Error = "[SESSION SERVICE] Invalid session."
                    };
                }
            }
            catch (Exception)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "[LOT MANAGEMENT MANAGER] Could not add lot."
                };
            }
        }

        public ResponseDTO<Boolean> DeleteLot(Guid ownerid, string lotname)
        {
            try
            {
                ResponseDTO<Boolean> response = _lotManagementService.DeleteLot(ownerid, lotname);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "[LOT MANAGEMENT MANAGER] Could not delete lot."
                };
            }
        }

        public ResponseDTO<Lot> GetLotByName(Guid ownerid, string lotname)
        {
            try
            {
                ResponseDTO<Lot> response = _lotManagementService.GetLotByName(ownerid, lotname);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<Lot>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT MANAGER] Could not get lot."
                };
            }
        }

        public ResponseDTO<List<Lot>> GetAllLots(string sessionIdString)
        {
       
            ResponseDTO<List<Lot>> response = _lotManagementService.GetAllLots();
            

            // Check if token is in Guid Format
            Guid sessionId;
            try
            {
                sessionId = Guid.Parse(sessionIdString);
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Lot>>()
                {
                    Data = null,
                    Error = "tokenString not a valid Guid"
                };
            }

            ResponseDTO<Session> sessionDTO = _sessionServices.GetSession(sessionId);

            // If session data is null, then an error occured
            if (sessionDTO.Data == null)
            {
                return new ResponseDTO<List<Lot>>()
                {
                    Data = null,
                    Error = sessionDTO.Error

                };
            }

            return _lotManagementService.GetAllLots();
        }

        public ResponseDTO<List<Lot>> GetAllLotsByOwner(Guid ownerid)
        {
            try
            {
                ResponseDTO<List<Lot>> response = _lotManagementService.GetAllLotsByOwner(ownerid);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Lot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT MANAGER] Could not get lots."
                };
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpots()
        {
            try
            {
                ResponseDTO<List<Spot>> response = _lotManagementService.GetAllSpots();
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Spot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT MANAGER] Could not get spots."
                };
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByOwner(Guid ownerid)
        {
            try
            {
                ResponseDTO<List<Spot>> response = _lotManagementService.GetAllSpotsByOwner(ownerid);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Spot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT MANAGER] Could not get spots."
                };
            }

        }

        public ResponseDTO<List<Spot>> GetAllSpotsByLot(ReservationRequestDTO request)
        {
            // Check if sessionId is in Guid Format
            Guid sessionId;
            try
            {
                sessionId = Guid.Parse(request.SessionId);
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Spot>>()
                {
                    Data = null,
                    Error = "tokenString not a valid Guid"
                };
            }

            ResponseDTO<Session> sessionDTO = _sessionServices.GetSession(sessionId);

            // If session data is null, then an error occured
            if (sessionDTO.Data == null)
            {
                return new ResponseDTO<List<Spot>>()
                {
                    Data = null,
                    Error = sessionDTO.Error

                };
            }

            // Check if LotId is formatted properly
            Guid lotId;
            try
            {
                lotId = Guid.Parse(request.LotId);
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Spot>>()
                {
                    Data = null,
                    Error = "OwnerId not in proper Guid format."
                };
            }

            return _lotManagementService.GetAllSpotsByLot(lotId);
        }

    }
}