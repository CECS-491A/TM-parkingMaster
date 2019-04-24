using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Manager.Managers
{
    public class ReservationManager : IReservationManager
    {
        private ReservationServices _reservationServices;
        private SessionService _sessionServices;

        public ReservationManager()
        {
            _reservationServices = new ReservationServices();
        }

        public ResponseDTO<bool> ReserveSpot(ReservationRequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
}