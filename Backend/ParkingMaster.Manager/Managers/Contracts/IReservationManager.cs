using System;
using System.Collections.Generic;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Manager.Managers
{
    public interface IReservationManager
    {
        ResponseDTO<bool> ReserveSpot(ReservationRequestDTO request);
    }
}