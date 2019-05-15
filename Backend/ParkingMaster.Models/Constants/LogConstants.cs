using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Constants
{
    public static class LogConstants
    {
        //FORMAT CONSTANTS
        public static readonly string DATE_FORMAT = "yyyy_MM_dd";
        public static string temp = @"C:\LOGS\LOGS";
        //FILE & DIRECTORY CONSTANTS
        public static readonly string PATH_LOG_FAILURES = @"C:\ParkingMaster\LOG_FAILURE_COUNT.json";
        public static readonly string PATH_LOG_DIRECTORY = @"C:\LOGS\LOGS";
        public static string PATH_LOG_CURRENT = @"C:\LOGS\Logs.json"; //OVERRIDABLE

        //LOG ACTION CONSTANTS
        public static readonly string ACTION_LOGIN = "Login";
        public static readonly string ACTION_LOGOUT = "Logout";
        public static readonly string ACTION_ADD_RESERVATION = "AddReservation";
        public static readonly string ACTION_EXTEND_RESERVATION = "ExtendReservation";
        public static readonly string ACTION_REGISTER_VEHICLE = "RegisterVehicle";
        public static readonly string ACTION_REGISTER_LOT = "RegisterLot";
        public static readonly string ACTION_DELETE_LOT = "DeleteLot";
        public static readonly string ACTION_ADD_VEHICLE = "StoreVehicle";

        //FAIL ACTIONS CONSTANTS
        public const string FAIL_LOGIN = "FailLogin";
        public const string FAIL_LOGOUT = "FailLogout";
    }
}
