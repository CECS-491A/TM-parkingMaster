using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models; // Add reference to ParkingMaster.DataAccess
using System.Reflection;

namespace ParkingMaster.Services
{
    /// <summary>
    /// Check nullity of object parameters. 
    /// If all parameters non-null, add to database and return true.
    /// Otherwise, return false.
    /// </summary>
    public class UserCreationService
    {
        public Boolean UserCreation(object o)
        {
            var objlist = new List<object>();
            if (o != null) // check object is null
            {
                var properties = o.GetType().GetProperties();
                foreach (var p in properties)
                {
                    if (p.GetValue(o) == null) { return false; } // if any values null, cannot add
                }
                objlist.Add(o); // UserRepository.AddUser(user)
                return true;
            }
            return false; // object is null
        }
    }
}
