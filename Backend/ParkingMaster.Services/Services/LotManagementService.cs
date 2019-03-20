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

        public ResponseDTO<bool> AddLot(string lotname)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            // create new lot with owner GUID, lotname
            // response = _lotGateway.AddLot(lot);
            // return response;
            throw new NotImplementedException();
        }

        public ResponseDTO<bool> AddSpots() // might just add this into AddLot
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
