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
    public class LogController : ApiController
    {
        [HttpPost]
        [Route("api/admin/logs")]
        public IHttpActionResult GetAllLogs([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LogManager lm = new LogManager();
            ResponseDTO<List<Log>> list = lm.GetAllLogs(request);

            if(list.Data != null)
            {
                return Ok(list.Data);
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(list.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }
    }
}