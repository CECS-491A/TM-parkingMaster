using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Services.Services
{
    public class ReservationServices : IReservationServices
    {
        private LotGateway _lotGateway;

        public ReservationServices()
        {
            _lotGateway = new LotGateway();
        }

        public ReservationServices(UserContext context)
        {
            _lotGateway = new LotGateway(context);
        }

        public ResponseDTO<bool> ReserveSpot(ReservationDTO reservation)
        {
            return _lotGateway.ReserveSpot(reservation);
        }

        public ResponseDTO<List<UserSpotDTO>> GetAllUserResverations(Guid userId)
        {
            return _lotGateway.GetAllUserReservations(userId);
        }
    }
}
