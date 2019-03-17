using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Models.DTO
{
    public class ClaimDTO : IEquatable<ClaimDTO>
    {
        public string Title { get; set; }
        public string Value { get; set; }

        public ClaimDTO(string t, string v)
        {
            Title = t;
            Value = v;
        }

        public ClaimDTO(Claim claim)
        {
            Title = claim.Title;
            Value = claim.Value;
        }

        public bool Equals(ClaimDTO obj)
        {
            if (obj == null)
            {
                return false;
            }

            ClaimDTO otherClaim = obj as ClaimDTO;

            if (otherClaim != null)
            {
                if (this.Title.Equals(otherClaim.Title))
                {
                    return this.Value.Equals(otherClaim.Value);
                };
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            ClaimDTO otherClaim = obj as ClaimDTO;

            if (otherClaim != null)
            {
                if (this.Title.Equals(otherClaim.Title))
                {
                    return this.Value.Equals(otherClaim.Value);
                };
            }

            return false;
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

    }
}
