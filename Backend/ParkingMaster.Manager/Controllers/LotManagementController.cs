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
    public class LotManagementController : ApiController
    {
        [HttpPut]
        [Route("ParkingMaster/api/lot/register")]
        public IHttpActionResult CreateLot() // [FromBody, Required] ParkingMasterFrontendDTO request
        {
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();
            var httpRequest = HttpContext.Current.Request;

            try
            {
                ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(httpRequest["token"]);

                if (response.Data != null)
                {
                    Guid ownerid = new Guid(response.Data.Id);
                    var username = httpRequest["username"];
                    var lotname = httpRequest["lotname"];
                    var address = httpRequest["address"];
                    var cost = Convert.ToDouble(httpRequest["cost"]);
                    var spotfile = httpRequest.Files["file"];
                    var spotmap = httpRequest.Files["map"];
                    ResponseDTO<Boolean> result = lotManagementManager.AddLot(ownerid, lotname, address, cost, spotfile);
                    return Ok(result.Data);

                }
                else
                {
                    return Content((HttpStatusCode)404, response.Error);
                }
            }
            catch (Exception e)
            {
                return Content((HttpStatusCode)400, e.Message);
            }

        }

        [HttpDelete]
        [Route("ParkingMaster/api/lot/delete")] // api/user/lot/delete
        public IHttpActionResult DeleteLot([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                return Ok(response.Data);

            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }

        /*
        [HttpPut]
        [Route("")] // api/user/lot/edit
        public IHttpActionResult EditLot([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                return Ok(response.Data);

            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }
        

        [HttpGet]
        [Route("ParkingMaster/api/lot/all")] // api/user/lot
        public IHttpActionResult GetAllLots([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                return Ok(response.Data);

            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }

        [HttpGet]
        [Route("ParkingMaster/api/lot/all/owner")]
        public IHttpActionResult GetLotsByOwner([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                return Ok(response.Data);

            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }

        [HttpGet]
        [Route("ParkingMaster/api/spot/all")]
        public IHttpActionResult GetAllSpots([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                return Ok(response.Data);

            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }

        [HttpGet]
        [Route("ParkingMaster/api/spot/all/owner")]
        public IHttpActionResult GetSpotsByOwner([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                return Ok(response.Data);

            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }

        [HttpGet]
        [Route("ParkingMaster/api/spot/lot")]
        public IHttpActionResult GetSpotsByLot([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                return Ok(response.Data);

            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }*/

    }
}