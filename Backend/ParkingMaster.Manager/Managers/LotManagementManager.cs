//using ParkingMaster.DataAccess.Context;
using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Gateways;
using ParkingMaster.Models.Constants;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Security.Authorization;
using ParkingMaster.Services.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ParkingMaster.Manager.Managers
{
    public class LotManagementManager
    {
        private readonly LotManagementService _lotManagementService;
        private readonly SessionService _sessionServices;
        UserContext _dbcontext;

        public LotManagementManager()
        {
            _lotManagementService = new LotManagementService();
            _sessionServices = new SessionService();
        }

        public LotManagementManager(UserContext context)
        {
            _dbcontext = context;
            _lotManagementService = new LotManagementService(_dbcontext);
            _sessionServices = new SessionService();
        }

        public ResponseDTO<Boolean> AddLot(HttpRequest httprequest)
        {
            //ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(httpRequest["token"]);
            try
            {
                var token = httprequest["token"];
                ResponseDTO<Session> SessionDTO = _sessionServices.GetSession(new Guid(token));
                if (SessionDTO.Data != null)
                {
                    AuthorizationClient authorizationClient = new AuthorizationClient();
                    List<ClaimDTO> functionClaims = new List<ClaimDTO>()
                    {
                        new ClaimDTO(new Claim("Action", "AddParkingLot"))
                    };
                    var username = httprequest["username"]; 
                    ResponseDTO<Boolean> authCheck = authorizationClient.Authorize(username, functionClaims);

                    if (authCheck.Data == true)
                    {
                        //String[] arr = httprequest.Form.AllKeys;
                        var ownerid = SessionDTO.Data.UserAccount.Id;
                        var lotname = httprequest["lotname"];
                        var address = httprequest["address"];
                        var cost = Double.Parse(httprequest["cost"]);
                        var useraccount = SessionDTO.Data.UserAccount;
                        var spotfile = httprequest.Files["file"];
                        var mapfile = httprequest.Files["map"];
                        ResponseDTO<Boolean> response = _lotManagementService.AddLot(ownerid, lotname, address, cost, useraccount, spotfile, mapfile);
                        return response;
                    }
                    else
                    {
                        return new ResponseDTO<Boolean>()
                        {
                            Data = false,
                            Error = ErrorStrings.UNAUTHORIZED_ACTION
                        };
                    }

                }
                else
                {
                    return new ResponseDTO<Boolean>()
                    {
                        Data = false,
                        Error = ErrorStrings.SESSION_NOT_FOUND
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = httprequest.Form + e.ToString()
                };
            }
        }

        public ResponseDTO<Boolean> DeleteLot(HttpRequest httprequest)
        {
            try
            {
                var token = httprequest["token"];
                ResponseDTO<Session> SessionDTO = _sessionServices.GetSession(new Guid(token));
                if (SessionDTO.Data != null)
                {
                    AuthorizationClient authorizationClient = new AuthorizationClient();
                    List<ClaimDTO> functionClaims = new List<ClaimDTO>()
                    {
                        new ClaimDTO(new Claim("Action", "DeleteParkingLot"))
                    };
                    var username = httprequest["username"];
                    ResponseDTO<Boolean> authCheck = authorizationClient.Authorize(username, functionClaims);
                    if (authCheck.Data == true)
                    {
                        var ownerid = SessionDTO.Data.UserAccount.Id;
                        var lotname = httprequest["lotname"];
                        ResponseDTO<Boolean> response = _lotManagementService.DeleteLot(ownerid, lotname);
                        return response;
                    }
                    else
                    {
                        return new ResponseDTO<Boolean>()
                        {
                            Data = false,
                            Error = ErrorStrings.UNAUTHORIZED_ACTION
                        };
                    }
                }
                else
                {
                    return new ResponseDTO<Boolean>()
                    {
                        Data = false,
                        Error = ErrorStrings.SESSION_EXPIRED
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "[LOT MANAGEMENT MANAGER] Could not delete lot. " + e.ToString()
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

        public ResponseDTO<List<Lot>> GetAllLotsByOwner(HttpRequest httprequest)
        {
            try
            {

                var token = httprequest["token"];
                ResponseDTO<Session> SessionDTO = _sessionServices.GetSession(new Guid(token));
                if (SessionDTO.Data != null)
                {
                    var ownerid = SessionDTO.Data.UserAccount.Id;
                    ResponseDTO<List<Lot>> response = _lotManagementService.GetAllLotsByOwner(ownerid);
                    return response;
                }
                else
                {
                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = null,
                        Error = SessionDTO.Error
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseDTO<List<Lot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT MANAGER] Could not fetch lots. " + e.ToString()
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

        public ResponseDTO<LotResponseDTO> GetAllSpotsByLot(ReservationRequestDTO request)
        {
            // Check if sessionId is in Guid Format
            Guid sessionId;
            try
            {
                sessionId = Guid.Parse(request.SessionId);
            }
            catch (Exception)
            {
                return new ResponseDTO<LotResponseDTO>()
                {
                    Data = null,
                    Error = "tokenString not a valid Guid"
                };
            }

            ResponseDTO<Session> sessionDTO = _sessionServices.GetSession(sessionId);

            // If session data is null, then an error occured
            if (sessionDTO.Data == null)
            {
                return new ResponseDTO<LotResponseDTO>()
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
                return new ResponseDTO<LotResponseDTO>()
                {
                    Data = null,
                    Error = "OwnerId not in proper Guid format."
                };
            }

            // Get parking lot information for reading the parking lot map
            ResponseDTO<Lot> lotResponse = _lotManagementService.GetLotByLotId(lotId);
            if(lotResponse.Data == null)
            {
                return new ResponseDTO<LotResponseDTO>()
                {
                    Data = null,
                    Error = lotResponse.Error
                };
            }

            ResponseDTO<Image> lotMap = _lotManagementService.GetLotImage(lotResponse.Data.MapFilePath);

            if (lotMap.Data == null)
            {
                return new ResponseDTO<LotResponseDTO>()
                {
                    Data = null,
                    Error = lotMap.Error
                };
            }

            // Convert image to a byte array
            byte[] mapBytes;
            using (var ms = new MemoryStream())
            {
                lotMap.Data.Save(ms, lotMap.Data.RawFormat);
                mapBytes =  ms.ToArray();
            }

            // Get the list of parking spots
            ResponseDTO<List<Spot>> spotsList = _lotManagementService.GetAllSpotsByLot(lotId);

            if (spotsList.Data == null)
            {
                return new ResponseDTO<LotResponseDTO>()
                {
                    Data = null,
                    Error = spotsList.Error
                };
            }

            // Set up data to be returned
            LotResponseDTO lotResponseDTO = new LotResponseDTO()
            {
                SpotsList = spotsList.Data,
                LotMap = mapBytes
            };

            return new ResponseDTO<LotResponseDTO>()
            {
                Data = lotResponseDTO
            };

        }

    }
}