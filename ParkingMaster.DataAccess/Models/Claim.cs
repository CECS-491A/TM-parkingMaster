using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Models
{
    public class Claim : IEquatable<Claim>
    {
        public string title { get; set; }
        public string value { get; set; }

        public Claim(string t, string v)
        {
            title = t;
            value = v;
        }

        public Claim()
        {
            title = "DEFAULT";
            value = "DEFAULT";
        }

        public Boolean Equals(Claim obj)
        {
            if(obj == null)
            {
                return false;
            }

            Claim otherClaim = obj as Claim;

            if(otherClaim != null)
            {
                if (this.title.Equals(otherClaim.title))
                {
                    return this.value.Equals(otherClaim.value);
                };
            }

            return false;
        }
    }
}

