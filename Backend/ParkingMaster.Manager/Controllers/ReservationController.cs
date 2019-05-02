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
                ResponseDTO<HttpStatusCode> statuesResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statuesResponse.Data, statuesResponse.Error);
            }
        }
    }
}
