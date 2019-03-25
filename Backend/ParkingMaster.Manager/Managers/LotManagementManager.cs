using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Context;
using ParkingMaster.DataAccess.Gateways;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingMaster.Manager.Managers
{
    public class LotManagementManager
    {
        private ILotManagementService _lotManagementService;
        private readonly LotGateway _lotGateway;
        private readonly UserGateway _userGateway;
        //private readonly LotContext _lotContext;

        public LotManagementManager()
        {
            _lotGateway = new LotGateway();
            _userGateway = new UserGateway();
            _lotManagementService = new LotManagementService(_lotGateway, _userGateway);
        }

        // 'The file collection is populated only when the HTTP request Content-Type value is "multipart/form-data"'
        public Boolean AddLot(Guid ownerid, string lotname, string address, double cost) // need another parameter, file
        {

            // PLEASE REPLACE THE FOLLOWING LINE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = Guid.NewGuid();

            ResponseDTO<Boolean> response = new ResponseDTO<Boolean>();
            response = _lotManagementService.AddLot(ownerid, lotname, address, cost); //(lotname, spotfile)
            return response.Data;
        }

        public Boolean AddSpots(Lot lot) 
        {
            ResponseDTO<Boolean> response = new ResponseDTO<Boolean>();
            //response = _lotManagementService.AddLot(lotname);
            return response.Data;
        }

        public Boolean DeleteLot(Guid ownerid, string lotname)
        {
            ResponseDTO<Boolean> response = new ResponseDTO<Boolean>();
            response = _lotManagementService.DeleteLot(ownerid, lotname);
            return response.Data;
        }

        public Boolean EditLot(string lotname)
        {
            return false;
        }



    }
}