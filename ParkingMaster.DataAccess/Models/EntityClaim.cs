using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess.Models
{
    public class EntityClaim
    {
        public string Owner { get; set; }
        public Claim _Claim { get; set; }

        public EntityClaim()
        {
            Owner = "";
            _Claim = null;
        }

        public EntityClaim(string s, Claim c)
        {
            Owner = s;
            _Claim = c;
        }

        public override string ToString()
        {
            return "Owner: " + Owner + " | Claim: " + _Claim.Title + " : " + _Claim.Value; 
        }

    }
}
