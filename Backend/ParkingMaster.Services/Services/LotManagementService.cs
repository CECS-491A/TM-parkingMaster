using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Gateways;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Services.Services
{
    public class LotManagementService : ILotManagementService
    {

        private LotGateway _lotGateway;

        public LotManagementService(LotGateway lotGateway)
        {
            _lotGateway = lotGateway;
        }

        public ResponseDTO<bool> AddLot(string lotname) // (string lotname, ??? spotfile)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            Lot newLot = new Lot()
            {
                LotId = Guid.NewGuid(),
                LotName = lotname,
                Spots = new List<Spot>() // Spots = new List<Spot>()
                //OwnerId
                //UserAccount
            };

            response = _lotGateway.AddLot(newLot); // (newLot, parsed list?)
            return response;
        }

        public List<Spot> ParseSpotsFromFile() // parameter = file
        {
            //List<Spot> spotList = new List<Spot>();
            return new List<Spot>();
        }

        public ResponseDTO<bool> AddSpots()
        {
            // parse CSV, create list of spots, set lot's spots to this list
            throw new NotImplementedException();
        }

        public ResponseDTO<bool> DeleteLot(string lotname)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<bool> EditLot(string lotname)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<bool> EditSpots()
        {
            throw new NotImplementedException();
        }

        public List<Lot> GetAllLots()
        {
            throw new NotImplementedException();
        }

        public List<Lot> GetAllLotsByOwner(string ownername)
        {
            throw new NotImplementedException();
        }

        public List<Spot> GetAllSpots()
        {
            throw new NotImplementedException();
        }

        public List<Spot> GetAllSpotsInLot(string lotname)
        {
            throw new NotImplementedException();
        }

        public Lot GetLotByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
