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
using ParkingMaster.Services.Services;

namespace ParkingMaster.Manager.Controllers
{
    public class SsoController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult SsoLogin([FromBody, Required] SsoUserRequestDTO request)
        {
            LoginManager loginManager = new LoginManager();

            ResponseDTO<Session> response = loginManager.SsoLogin(request);

            if (response.Data != null)
            {
                return Redirect("http://localhost:8080/#/login?token=" + response.Data.SessionId.ToString("D"));
            }
            else
            {
                ResponseDTO<HttpStatusCode> statusResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statusResponse.Data, statusResponse.Error);
            }
        }

        [HttpPost]
        [Route("api/sso/deleteuser")]
        public IHttpActionResult DeleteUser([FromBody, Required] SsoUserRequestDTO request)
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

        [HttpPost]
        [Route("api/sso/logoutuser")]
        public IHttpActionResult LogoutUser([FromBody, Required] SsoUserRequestDTO request)
        {
            if (request == null)
            {
                return Content((HttpStatusCode)400, "Request is null.");
            }

            UserManagementManager _userManagementManager = new UserManagementManager();

            ResponseDTO<HttpStatusCode> managerResponse = _userManagementManager.LogoutUser(request);

            if (managerResponse.Data != (HttpStatusCode)200)
            {
                return Content(managerResponse.Data, managerResponse.Error);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("api/sso/healthcheck")]
        public IHttpActionResult HealthCheck()
        {
            DatabaseCheckerService _databaseChecker = new DatabaseCheckerService();
            ResponseDTO<bool> response = _databaseChecker.CheckConnection();

            if (!response.Data)
            {
                ResponseDTO<HttpStatusCode> statuesResponse = ResponseManager.ConvertErrorToStatus(response.Error);
                return Content(statuesResponse.Data, statuesResponse.Error);
            }
            else
            {
                return Ok();
            }
        }
    }
}