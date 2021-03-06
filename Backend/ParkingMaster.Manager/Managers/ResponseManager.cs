﻿using System;
using ParkingMaster.Models.DTO;
using ParkingMaster.Services.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Text;
using ParkingMaster.Models.Constants;

namespace ParkingMaster.Manager.Managers
{
    public class ResponseManager
    {

        public static ResponseDTO<HttpStatusCode> ConvertErrorToStatus(string error)
        {
            /* Custom Status Codes in use:
             *      401 - Will end user session in the frontend
             *      418 - User is Disabled
             *      419 - User has yet to accept TOS
             */
            switch (error)
            {
                // Error 0
                case ErrorStrings.SESSION_EXPIRED:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)401,
                        Error = "Invalid session provided."
                    };

                // Error 1
                case ErrorStrings.SESSION_NOT_FOUND:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)401,
                        Error = "Invalid session provided."
                    };

                // Error 2
                case ErrorStrings.UNAUTHORIZED_ACTION:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)403,
                        Error = "User does not have permission to access function."
                    };

                // Error 3
                case ErrorStrings.REQUEST_FORMAT:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)400,
                        Error = "Invalid request formatting."
                    };

                // Error 4
                case ErrorStrings.FAILED_CONNECTION_CHECK:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)500,
                        Error = "Data access error."
                    };

                // Error 5
                case ErrorStrings.DATA_ACCESS_ERROR:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)500,
                        Error = "Data access error."
                    };

                // Error 6
                case ErrorStrings.OLD_SSO_REQUEST:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)425,
                        Error = "Request timestamp is too old."
                    };

                // Error 7
                case ErrorStrings.NO_FUNCTION_TO_AUTHORIZE:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)500,
                        Error = "Improper use of Authorization Client."
                    };

                // Error 8
                case ErrorStrings.RESOURCE_NOT_FOUND:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)400,
                        Error = "Resource not found."
                    };

                // Error 9
                case ErrorStrings.USER_NOT_FOUND:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)400,
                        Error = "Resource not found."
                    };

                // Error 10
                case ErrorStrings.USER_DISABLED:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)418,
                        Error = "User is currently disabled."
                    };

                // Error 11
                case ErrorStrings.USER_TOS_NOT_ACCEPTED:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)419,
                        Error = "User must accept the TOS before proceeding."
                    };

                default:
                    return new ResponseDTO<HttpStatusCode>()
                    {
                        Data = (HttpStatusCode)400,
                        Error = error
                    };
            }
        }
    }
}