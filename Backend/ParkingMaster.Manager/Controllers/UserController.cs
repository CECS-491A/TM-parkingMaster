﻿using System;
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

namespace ParkingMaster.Manager.Controllers
{

    public class UserController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult SsoLogin([FromBody, Required] SsoUserRequestDTO request)
        {
            LoginManager loginManager = new LoginManager();
            
            ResponseDTO<Session> response = loginManager.SsoLogin(request);

            if (response.Data != null)
            {
                return Ok(new { redirectURL = "http://localhost:8080/#/login?token=" + response.Data.SessionId.ToString("D")});
            }
            else
            {
                return Content((HttpStatusCode)404, response.Error);
            }
        }

        [HttpPost]
        [Route("api/user/session")]
        public IHttpActionResult Session([FromBody, Required] ParkingMasterFrontendDTO request)
        {
            LoginManager loginManager = new LoginManager();

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
