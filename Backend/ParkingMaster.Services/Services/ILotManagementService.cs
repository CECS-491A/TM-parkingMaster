using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Services.Services
{
    public interface ILotManagementService
    {
        ResponseDTO<Boolean> AddLot(Guid ownerid, string lotname, string address, double cost);
        ResponseDTO<Boolean> DeleteLot(Guid ownerid, string lotname);
        ResponseDTO<Boolean> EditLotSpots(Guid ownerid, string lotname);
        ResponseDTO<Boolean> EditLotName(Guid ownerid, string oldlotname, string newlotname);

        Lot GetLotByName(Guid ownerid, string name);
        List<Spot> ParseSpotsFromFile();

        //ResponseDTO<Boolean> AddSpots(); // parse CSV - file argument. Probably going to remove this
        //ResponseDTO<Boolean> EditSpots(Guid ownerid, string lotname); // parse CSV - file argument

        List<Lot> GetAllLots();
        List<Lot> GetAllLotsByOwner(Guid ownerid); // use this + GetAllSpotsInLot to get all spots by user
        List<Spot> GetAllSpots();
        List<Spot> GetAllSpotsInLot(Guid ownerid, string lotname);
    }
}
