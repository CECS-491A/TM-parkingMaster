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
        private LotGateway _lotGateway;
        //private readonly LotContext _lotContext;

        public LotManagementManager()
        {
            _lotGateway = new LotGateway();
            _lotManagementService = new LotManagementService(_lotGateway);
            //_lotContext = new LotContext();
            //_lotManagementService = new LotManagementService(_lotContext);
        }

        public Boolean AddLot(string lotname) // merge with add lots?
        {
            ResponseDTO<Boolean> response = new ResponseDTO<Boolean>();
            response = _lotManagementService.AddLot(lotname); //(lotname, spotfile)
            return response.Data;
        }

        public Boolean AddSpots(Lot lot) // 'The file collection is populated only when the HTTP request Content-Type value is "multipart/form-data"'
        {
            ResponseDTO<Boolean> response = new ResponseDTO<Boolean>();
            //response = _lotManagementService.AddLot(lotname);
            return response.Data;
        }

        public Boolean DeleteLot(string lotname)
        {
            return false;
        }

        public Boolean EditLot(string lotname)
        {
            return false;
        }



    }
}