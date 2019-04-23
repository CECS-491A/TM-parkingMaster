using ParkingMaster.DataAccess.Gateways;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ParkingMaster.Manager.Managers // error handling and logging happens here?? or in controller
    // also logic...? business rules
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
        public ResponseDTO<Boolean> AddLot(Guid ownerid, string lotname, string address, double cost, FileInfo file) // need another parameter, file
        {

            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Boolean> response = _lotManagementService.AddLot(ownerid, lotname, address, cost, file); //(lotname, spotfile)
            return response;
        }

        public ResponseDTO<Boolean> DeleteLot(Guid ownerid, string lotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Boolean> response = _lotManagementService.DeleteLot(ownerid, lotname);
            return response;
        }

        public ResponseDTO<Boolean> EditLotSpots(Guid ownerid, string lotname, FileInfo file)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Boolean> response = _lotManagementService.EditLotSpots(ownerid, lotname, file);
            return response;
        }

        public ResponseDTO<Boolean> EditLotName(Guid ownerid, string oldlotname, string newlotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Boolean> response = _lotManagementService.EditLotName(ownerid, oldlotname, newlotname);
            return response;
        }

        public ResponseDTO<Lot> GetLotByName(Guid ownerid, string lotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<Lot> response = _lotManagementService.GetLotByName(ownerid, lotname);
            return response;
        }

        public ResponseDTO<List<Lot>> GetAllLots()
        {
            ResponseDTO<List<Lot>> response = _lotManagementService.GetAllLots();
            return response;
        }

        public ResponseDTO<List<Lot>> GetAllLotsByOwner(Guid ownerid)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<List<Lot>> response = _lotManagementService.GetAllLotsByOwner(ownerid);
            return response;
        }

        public ResponseDTO<List<Spot>> GetAllSpots()
        {
            ResponseDTO<List<Spot>> response = _lotManagementService.GetAllSpots();
            return response;
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByOwner(Guid ownerid)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<List<Spot>> response = _lotManagementService.GetAllSpotsByOwner(ownerid);
            return response;
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByLot(Guid ownerid, string lotname)
        {
            // PLEASE REPLACE THE FOLLOWING LINE WITH APPROPRIATE CODE ONCE WE CAN EXTRACT GUID FROM LOGIN TOKENS //
            ownerid = new Guid();

            ResponseDTO<List<Spot>> response = _lotManagementService.GetAllSpotsByLot(ownerid, lotname);
            return response;
        }

    }
}