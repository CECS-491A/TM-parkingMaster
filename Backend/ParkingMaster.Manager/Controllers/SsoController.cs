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

namespace ParkingMaster.Manager.Controllers
{
    public class SsoController : ApiController
    {
        [HttpPost]
        [Route("api/sso/deleteuser")]
        public IHttpActionResult SsoLogin([FromBody, Required] SsoUserRequestDTO request)
        {
            if(request == null)
            {
                return Content((HttpStatusCode)400, "Request is null.");
            }

            UserManagementManager _userManagementManager = new UserManagementManager();

            ResponseDTO<HttpStatusCode> managerResponse = _userManagementManager.DeleteUser(request);

            if(managerResponse.Data != (HttpStatusCode)200)
            {
                return Content(managerResponse.Data, managerResponse.Error);
            }
            else
            {
                return Ok();
            }
        }
    }
}