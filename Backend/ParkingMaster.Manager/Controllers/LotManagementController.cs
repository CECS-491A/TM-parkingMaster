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

namespace ParkingMaster.Manager.Controllers
{
    public class LotManagementController : ApiController
    {
        [HttpPost]
        [Route("")] // api/user/lot/create
        public IHttpActionResult CreateLot([FromBody, Required] ParkingMasterFrontendDTO request)
        {

            /*
             * append everything to form data and just parse from there
             */
            LoginManager loginManager = new LoginManager();
            LotManagementManager lotManagementManager = new LotManagementManager();

            ResponseDTO<ParkingMasterFrontendDTO> response = loginManager.SessionChecker(request.Token);

            if (response.Data != null)
            {
                Guid ownerid = new Guid(request.Id);
                //string 
                //response = lotManagementManager.AddLot(ownerid, lotname, address, cost);
                return Ok(response.Data);

            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }

        }

        [HttpDelete]
        [Route("")] // api/user/lot/delete
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
        [Route("")] // api/user/lot
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
        [Route("")]
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
        [Route("")]
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
        [Route("")]
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
        [Route("")]
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
        }

    }
}