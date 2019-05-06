using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Constants
{
    public class ErrorStrings
    {
        public const string SESSION_EXPIRED = "Session has expired.";
        public const string SESSION_NOT_FOUND = "Session does not exist.";
        public const string LOT_NOT_FOUND = "Parking lot does not exist.";
        public const string UNAUTHORIZED_ACTION = "User is not allowed to perform requested action.";
        public const string REQUEST_FORMAT = "Invalid request format.";
        public const string FAILED_CONNECTION_CHECK = "Database connection check failed.";
        public const string DATA_ACCESS_ERROR = "Error when interacting with the data store.";
    }
}
