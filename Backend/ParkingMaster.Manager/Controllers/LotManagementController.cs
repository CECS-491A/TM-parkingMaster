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
using ParkingMaster.DataAccess;

namespace ParkingMaster.Manager.Controllers
{
    public class LotManagementController : ApiController
    {
        [HttpPut]
        [Route("api/lot/register")]
        public IHttpActionResult CreateLot() // [FromBody, Required] ParkingMasterFrontendDTO request
        {
            using (var dbcontext = new UserContext())
            {
                //LotManagementManager lotManagementManager = new LotManagementManager();
                LotManagementManager lotManagementManager = new LotManagementManager(dbcontext);
                var httprequest = HttpContext.Current.Request;

                try
                {
                    ResponseDTO<Boolean> response = lotManagementManager.AddLot(httprequest);
                    if (response.Data == true)
                    {
                        dbcontext.SaveChanges();
                        return Ok(response.Data);
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

        }

        [HttpGet]
        [Route("api/lot/delete")]
        public IHttpActionResult ShowAllLotsToDelete([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            using (var dbcontext = new UserContext())
            {
                LotManagementManager lotManagementManager = new LotManagementManager(dbcontext);
                var httprequest = HttpContext.Current.Request;

                try
                {
                    ResponseDTO<List<Lot>> response = lotManagementManager.GetAllLotsByOwner(httprequest);
                    if (response.Data != null)
                    {
                        return Ok(response.Data);
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
        }

        [HttpDelete]
        [Route("api/lot/delete")] // api/user/lot/delete
        public IHttpActionResult DeleteLot([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            using (var dbcontext = new UserContext())
            {
                LotManagementManager lotManagementManager = new LotManagementManager(dbcontext);
                var httprequest = HttpContext.Current.Request;

                try
                {
                    ResponseDTO<Boolean> response = lotManagementManager.DeleteLot(httprequest);
                    if (response.Data == true)
                    {
                        return Ok(response.Data);
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
        */
        [HttpPost]
        [Route("api/lotManagement/getAllLots")] // api/user/lot
        public IHttpActionResult GetAllLots([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            using (var dbcontext = new UserContext())
            {
                LotManagementManager lotManagementManager = new LotManagementManager(dbcontext);

                ResponseDTO<List<Lot>> response = lotManagementManager.GetAllLots(request.Token);

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
        /*
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
        */
        [HttpPost]
        [Route("api/lotManagement/getSpotsByLot")]
        public IHttpActionResult GetSpotsByLot([FromBody, Required] ReservationRequestDTO request)
        {
            using (var dbcontext = new UserContext())
            {
                LotManagementManager lotManagementManager = new LotManagementManager(dbcontext);

                ResponseDTO<LotResponseDTO> response = lotManagementManager.GetAllSpotsByLot(request);

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
}