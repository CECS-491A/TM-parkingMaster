using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Models
{
    public class Function
    {
        public string Name { get; set; }
        public Boolean Active { get; set; }

        public Function()
        {
            Name = "";
            Active = false;
        }

        public Function (string n, Boolean a)
        {
            Name = n;
            Active = a;
        }

        public override string ToString()
        {
            return "Name: " + Name + " Active: " + Active;
        }

    }
}
