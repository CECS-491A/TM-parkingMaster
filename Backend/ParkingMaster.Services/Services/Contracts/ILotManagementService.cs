﻿using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParkingMaster.Services.Services
{
    public interface ILotManagementService
    {
        ResponseDTO<Boolean> AddLot(Guid ownerid, string lotname, string address, double cost, UserAccount useraccount, HttpPostedFile spotfile, HttpPostedFile mapfile);
        ResponseDTO<Boolean> DeleteLot(Guid lotId);
        //ResponseDTO<Boolean> EditLotSpots(Guid ownerid, string lotname, FileInfo file);
        //ResponseDTO<Boolean> EditLotName(Guid ownerid, string oldlotname, string newlotname);

        ResponseDTO<Lot> GetLotByName(Guid ownerid, string name);
        List<Spot> ParseSpotsFromFile(Guid lotid, string lotname, HttpPostedFile file);

        //ResponseDTO<Boolean> AddSpots(); // parse CSV - file argument. Probably going to remove this
        //ResponseDTO<Boolean> EditSpots(Guid ownerid, string lotname); // parse CSV - file argument

        ResponseDTO<List<Lot>> GetAllLots();
        ResponseDTO<List<Lot>> GetAllLotsByOwner(Guid ownerid); // use this + GetAllSpotsInLot to get all spots by user
        ResponseDTO<List<Spot>> GetAllSpots();
        ResponseDTO<List<Spot>> GetAllSpotsByOwner(Guid ownerid);
        ResponseDTO<List<Spot>> GetAllSpotsByLot(Guid lotId);

    }
}
