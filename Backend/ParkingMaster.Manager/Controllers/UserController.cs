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

    public class UserController : ApiController
    {
        [HttpPost]
        [Route("api/user/ssologin")]
        public IHttpActionResult SsoLogin([FromBody, Required] LoginRequestDTO request)
        {
            LoginManager loginManager = new LoginManager();
            
            ResponseDTO<Session> response = loginManager.SsoLogin(request);

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
