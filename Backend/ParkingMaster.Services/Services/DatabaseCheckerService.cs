using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Constants;

namespace ParkingMaster.Services.Services
{
    public class DatabaseCheckerService
    {
        private UserContext context;

        public DatabaseCheckerService()
        {
            context = new UserContext();
        }

        public ResponseDTO<bool> CheckConnection()
        {
            try
            {
                context.Database.Connection.Open();
                context.Database.Connection.Close();
            }
            catch(Exception e)
            {
                return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = ErrorStrings.FAILED_CONNECTION_CHECK
                };
            }
            return new ResponseDTO<bool>()
            {
                Data = true
            };
        }
    }
}
