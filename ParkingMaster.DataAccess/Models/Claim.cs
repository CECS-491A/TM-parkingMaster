using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Models
{
    public class Claim : IEquatable<Claim>
    {
        public string Title { get; set; }
        public string Value { get; set; }

        public Claim(string t, string v)
        {
            Title = t;
            Value = v;
        }

        public Claim()
        {
            Title = "DEFAULT";
            Value = "DEFAULT";
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
                if (this.Title.Equals(otherClaim.Title))
                {
                    return this.Value.Equals(otherClaim.Value);
                };
            }

            return false;
        }
    }
}

