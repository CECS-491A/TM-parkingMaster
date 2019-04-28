using System;
using System.Collections.Generic;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Services.Services
{
    public interface IReservationServices
    {
        ResponseDTO<bool> ReserveSpot(ReservationDTO reservation);
    }
}
