using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Models
{
    // Just providing null values, probably going to delete this later
    public class BaseDTO
    {
        public static string Email_NullValue = null;
        public static string Password_NullValue = null;
        public static string DateOfBirth_NullValue = null; //DateTime.MinValue;
        public static string City_NullValue = null;
        public static string State_NullValue = null;
        public static string Country_NullValue = null;
    }
}
