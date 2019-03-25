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
            ResponseDTO<bool> response = new ResponseDTO<bool>();
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
            response = _lotGateway.AddLot(newLot, newLot.Spots);
            return response;
        }

        //public ResponseDTO<bool> AddSpots() // might just remove this
        //{
        //    throw new NotImplementedException();
        //}

        public ResponseDTO<bool> DeleteLot(Guid ownerid, string lotname)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<bool> EditLotSpots(Guid ownerid, string lotname)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<bool> EditLotName(Guid ownerid, string oldlotname, string newlotname)
        {
            throw new NotImplementedException();
        }

        public Lot GetLotByName(Guid ownerid, string name)
        {
            throw new NotImplementedException();
        }

        public List<Spot> ParseSpotsFromFile() // parameter = file
        {
            //List<Spot> spotList = new List<Spot>();


            //return spotList;

            return new List<Spot>();
        }

        public List<Lot> GetAllLots()
        {
            throw new NotImplementedException();
        }

        public List<Lot> GetAllLotsByOwner(Guid ownerid)
        {
            throw new NotImplementedException();
        }

        public List<Spot> GetAllSpots()
        {
            throw new NotImplementedException();
        }

        public List<Spot> GetAllSpotsInLot(Guid ownerid, string lotname)
        {
            throw new NotImplementedException();
        }
    }
}
