using System;
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

            if(error == ErrorStrings.SESSION_EXPIRED || error == ErrorStrings.SESSION_NOT_FOUND)
            {
                return new ResponseDTO<HttpStatusCode>()
                {
                    Data = (HttpStatusCode)401, 
                    Error = "Invalid session provided."
                };
            }

            if(error == ErrorStrings.REQUEST_FORMAT)
            {
                return new ResponseDTO<HttpStatusCode>()
                {
                    Data = (HttpStatusCode)401,
                    Error = "Invalid request formatting."
                };
            }

            // Default to Bad Request
            return new ResponseDTO<HttpStatusCode>()
            {
                Data = (HttpStatusCode)400,
                Error = error
            };
        }
    }
}