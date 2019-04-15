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
        private readonly LotGateway _lotGateway;
        private readonly UserGateway _userGateway;

        public LotManagementManager()
        {
            _lotGateway = new LotGateway();
            _userGateway = new UserGateway();
            _lotManagementService = new LotManagementService(_lotGateway, _userGateway);
        }

        public ResponseDTO<Boolean> AddLot(Guid ownerid, string lotname, string address, double cost, FileInfo file)
        {
            try
            {
                ResponseDTO<Boolean> response = _lotManagementService.AddLot(ownerid, lotname, address, cost, file);
                return response;
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

        public ResponseDTO<Boolean> EditLotSpots(Guid ownerid, string lotname, FileInfo file)
        {
            try
            {
                ResponseDTO<Boolean> response = _lotManagementService.EditLotSpots(ownerid, lotname, file);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "[LOT MANAGEMENT MANAGER] Could not edit spots."
                };
            }
        }

        public ResponseDTO<Boolean> EditLotName(Guid ownerid, string oldlotname, string newlotname)
        {
            try
            {
                ResponseDTO<Boolean> response = _lotManagementService.EditLotName(ownerid, oldlotname, newlotname);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "[LOT MANAGEMENT MANAGER] Could not edit lot name."
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

        public ResponseDTO<List<Lot>> GetAllLots()
        {
            try
            {
                ResponseDTO<List<Lot>> response = _lotManagementService.GetAllLots();
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

        public ResponseDTO<List<Spot>> GetAllSpotsByLot(Guid ownerid, string lotname)
        {
            try
            {
                ResponseDTO<List<Spot>> response = _lotManagementService.GetAllSpotsByLot(ownerid, lotname);
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

    }
}