using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Gateways;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Services.Services
{
    public class LotManagementService : ILotManagementService
    {

        private LotGateway _lotGateway;
        private UserGateway _userGateway;

        public LotManagementService(LotGateway lotGateway, UserGateway userGateway)
        {
            _lotGateway = lotGateway;
            _userGateway = userGateway;
        }

        public ResponseDTO<bool> AddLot(Guid ownerid, string lotname, string address, double cost) // (string lotname, ??? spotfile)
        {
            Lot newLot = new Lot()
            {
                LotId = Guid.NewGuid(),
                OwnerId = ownerid,
                LotName = lotname,
                Address = address,
                Cost = cost,
                //UserAccount = _userGateway.GetUserByUsername(), // oops need to fix this. add a getUserByGUID?
                Spots = new List<Spot>() // Spots = ParseSpotsFromFile(spotfile);
            };
            ResponseDTO<bool> response = _lotGateway.AddLot(newLot, newLot.Spots);
            return response;
        }

        public ResponseDTO<bool> DeleteLot(Guid ownerid, string lotname)
        {
            ResponseDTO<bool> response = _lotGateway.DeleteLot(ownerid, lotname);
            return response;
        }

        public ResponseDTO<bool> EditLotSpots(Guid ownerid, string lotname)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            return response;
        }

        public ResponseDTO<bool> EditLotName(Guid ownerid, string oldlotname, string newlotname)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            //response = _lotGateway.DeleteLot(ownerid, lotname);
            return response;
        }

        public ResponseDTO<Lot> GetLotByName(Guid ownerid, string name)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<List<Spot>> ParseSpotsFromFile() // parameter = file
        {
            //List<Spot> spotList = new List<Spot>();
            ResponseDTO<List<Spot>> response = new ResponseDTO<List<Spot>>();

            //return spotList;

            return response;
        }

        public ResponseDTO<List<Lot>> GetAllLots()
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<List<Lot>> GetAllLotsByOwner(Guid ownerid)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<List<Spot>> GetAllSpots()
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByOwner(Guid ownerid)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<List<Spot>> GetAllSpotsInLot(Guid ownerid, string lotname)
        {
            throw new NotImplementedException();
        }
    }
}
