using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Constants
{
    public class Roles
    {
        public const string STANDARD = "standard";
        public const string LOTMANAGER = "lotmanager";
        public const string SECURITY = "security";
        public const string ADMINISTRATOR = "administrator";
        public const string UNASSIGNED = "unassigned";

        public static bool IsRole(string role)
        {
            if(role == STANDARD ||
                role == LOTMANAGER ||
                role == SECURITY ||
                role == ADMINISTRATOR ||
                role == UNASSIGNED)
            {
                return true;
            }

            return false;
        }

        public static bool IsFrontendSelectableRole(string role)
        {
            if (role == STANDARD ||
                role == LOTMANAGER)
            {
                return true;
            }

            return false;
        }
    }
}
