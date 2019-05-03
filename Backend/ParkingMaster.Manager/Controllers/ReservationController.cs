using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Manager.Managers;
using System.Web;

namespace ParkingMaster.Manager.Controllers
{
    public class ReservationController : ApiController
    {
        [HttpPost]
        [Route("api/reservation/add")] // api/user/lot
        public IHttpActionResult ReserveSpot([FromBody, Required] ReservationRequestDTO request)
        {
            ReservationManager _reservationManager = new ReservationManager();
            ResponseDTO<bool> response = _reservationManager.ReserveSpot(request);

            if (response.Data)
            {
                return Ok("Successfully reserved: " + request.SpotId);
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }

        [HttpPost]
        [Route("api/reservation/getall")] // api/user/lot
        public IHttpActionResult GetAllUserReservations([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            ReservationManager _reservationManager = new ReservationManager();
            ResponseDTO<List<UserSpotDTO>> response = _reservationManager.GetAllUserReservations(request);

            if (response.Data != null)
            {
                return Ok(response.Data);
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }

        [HttpPost]
        [Route("api/reservation/extend")] // api/user/lot
        public IHttpActionResult ExtendUserReservation([FromBody, Required] ReservationRequestDTO request)
        {
            ReservationManager _reservationManager = new ReservationManager();
            ResponseDTO<UserSpotDTO> response = _reservationManager.ExtendUserReservation(request);

            if (response.Data != null)
            {
                return Ok(response.Data);
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }
    }
}
