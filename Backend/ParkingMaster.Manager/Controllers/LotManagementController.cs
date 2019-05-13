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
        public IHttpActionResult CreateLot()
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
                        ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                        return Content(statusResponse.Data, statusResponse.Error);
                    }
                }
                catch (Exception e)
                {
                    return Content((HttpStatusCode)400, e.Message);
                }
            }

        }

        [HttpPost]
        [Route("api/lot/delete")]
        public IHttpActionResult DeleteLot([FromBody, Required] LotDeletionDTO request)
        {
            using (var dbcontext = new UserContext())
            {
                LotManagementManager lotManagementManager = new LotManagementManager(dbcontext);

                try
                {
                    ResponseDTO<Boolean> response = lotManagementManager.DeleteLot(request);
                    if (response.Data == true)
                    {
                        return Ok(response.Data);
                    }
                    else
                    {
                        ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                        return Content(statusResponse.Data, statusResponse.Error);
                    }
                }
                catch (Exception e)
                {
                    return Content((HttpStatusCode)400, e.Message);
                }
            }
        }

        [HttpPost]
        [Route("api/lotManagement/getAllLots")]
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
                    ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                    return Content(statusResponse.Data, statusResponse.Error);
                }
            }
            
        }

        [HttpPost]
        [Route("api/lotManagement/getAllLotsByOwner")]
        public IHttpActionResult GetAllLotsByOwner([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            using (var dbcontext = new UserContext())
            {
                LotManagementManager lotManagementManager = new LotManagementManager(dbcontext);
                //ResponseDTO<List<Lot>> response = lotManagementManager.GetAllLots(request.Token);
                try
                {
                    ResponseDTO<List<Lot>> response = lotManagementManager.GetAllLotsByOwner(request);
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
                catch (Exception e)
                {
                    return Content((HttpStatusCode)400, e.Message);
                }
            }
        }

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
                    ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                    return Content(statusResponse.Data, statusResponse.Error);
                }
            }
        }

    }
}