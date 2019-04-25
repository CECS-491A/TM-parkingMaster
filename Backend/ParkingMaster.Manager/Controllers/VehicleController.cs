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
    public class VehicleController : ApiController
    {
        [HttpPost]
        [Route("api/vehicle/getAllUserVehicles")]
        public IHttpActionResult GetAllUserVehicles([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            VehicleManager _vehicleManager = new VehicleManager();
            ResponseDTO<List<Vehicle>> response = _vehicleManager.GetAllUserVehicles(request);

            if (response.Data != null)
            {
                return Ok(response.Data);
            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }

        [HttpPost]
        [Route("api/vehicle/register")]
        public IHttpActionResult RegisterVehicle([FromBody, Required] VehicleRequestDTO request)
        {
            VehicleManager _vehicleManager = new VehicleManager();
            ResponseDTO<bool> response = _vehicleManager.StoreVehicle(request);

            if (response.Data)
            {
                return Ok(response.Data);
            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }
    }
}
