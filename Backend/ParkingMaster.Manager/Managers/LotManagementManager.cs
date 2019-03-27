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

namespace ParkingMaster.Manager.Managers // error handling and logging happens here!
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

            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Boolean> response = _lotManagementService.AddLot(ownerid, lotname, address, cost); //(lotname, spotfile)
            return response.Data;
        }

        public Boolean DeleteLot(Guid ownerid, string lotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Boolean> response = _lotManagementService.DeleteLot(ownerid, lotname);
            return response.Data;
        }

        public Boolean EditLotSpots(Guid ownerid, string lotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Boolean> response = _lotManagementService.EditLotSpots(ownerid, lotname);
            return response.Data;
        }

        public Boolean EditLotName(Guid ownerid, string oldlotname, string newlotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Boolean> response = _lotManagementService.EditLotName(ownerid, oldlotname, newlotname);
            return response.Data;
        }

        public Lot GetLotByName(Guid ownerid, string lotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Lot> response = _lotManagementService.GetLotByName(ownerid, lotname);
            return response.Data;
        }

        public List<Lot> GetAllLots()
        {
            List<Lot> lots = new List<Lot>();

            return new List<Lot>();
        }

        public List<Lot> GetAllLotsByOwner(Guid ownerid)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();


            return new List<Lot>();
        }

        public List<Spot> GetAllSpots()
        {
            return new List<Spot>();
        }

        public List<Spot> GetAllSpotsByOwner(Guid ownerid)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            return new List<Spot>();
        }

        public List<Spot> GetAllSpotsByLot(Guid ownerid, string lotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            return new List<Spot>();
        }

    }
}