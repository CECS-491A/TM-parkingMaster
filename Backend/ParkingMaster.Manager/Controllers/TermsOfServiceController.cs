using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Manager.Managers;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace ParkingMaster.Manager.Controllers
{
    public class TermsOfServiceController : ApiController
    {
        [HttpPost]
        [Route("api/tos/current")]
        public IHttpActionResult GetActiveTOS([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            TermsOfServiceManager _tosManager = new TermsOfServiceManager();

            ResponseDTO<TermsOfService> response = _tosManager.GetActiveTOS(request);

            if(response.Data != null)
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
        [Route("api/tos/useraccepted")]
        public IHttpActionResult UserAcceptedTOS([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            TermsOfServiceManager _tosManager = new TermsOfServiceManager();

            ResponseDTO<bool> response = _tosManager.UserAcceptTOS(request);

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
