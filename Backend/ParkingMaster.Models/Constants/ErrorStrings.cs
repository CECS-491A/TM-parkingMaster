using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Constants
{
    public class ErrorStrings
    {
        // Error numbers are meant to help insure that ResponseManager takes care of every error case
        public const string SESSION_EXPIRED = "Session has expired."; // Error 0
        public const string SESSION_NOT_FOUND = "Session does not exist."; // Error 1
        public const string UNAUTHORIZED_ACTION = "User is not allowed to perform requested action."; // Error 2
        public const string REQUEST_FORMAT = "Invalid request format."; // Error 3
        public const string FAILED_CONNECTION_CHECK = "Database connection check failed."; // Error 4
        public const string DATA_ACCESS_ERROR = "Error when interacting with the data store."; // Error 5
        public const string OLD_SSO_REQUEST = "Timestamp on request is too old to be processed."; // Error 6
        public const string NO_FUNCTION_TO_AUTHORIZE = "No function claim provided to authorization client."; // Error 7
        public const string LOT_NOT_FOUND = "Parking lot does not exist."; // Error 8
        public const string USER_NOT_FOUND = "Username does not exist."; // Error 9
    }
}
