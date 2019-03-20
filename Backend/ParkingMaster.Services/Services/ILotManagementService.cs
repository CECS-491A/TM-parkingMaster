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
        ResponseDTO<Boolean> AddLot(string lotname);
        ResponseDTO<Boolean> DeleteLot(string lotname);
        ResponseDTO<Boolean> EditLot(string lotname); // edit name

        Lot GetLotByName(string name);

        ResponseDTO<Boolean> AddSpots(); // parse CSV - file argument
        ResponseDTO<Boolean> EditSpots(); // parse CSV - file argument

        List<Lot> GetAllLots();
        List<Spot> GetAllSpots();
        List<Spot> GetAllSpotsInLot(string lotname);


    }
}
