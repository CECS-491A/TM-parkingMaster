using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Models.Models
{
    public class Claim : IEquatable<Claim>
    {
        [Key]
        public Guid ClaimId { get; set; }
        public Guid UserClaimsId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }

        //Foreign Key is necessary to activate cascading delete
        [ForeignKey("UserClaimsId")]
        public UserClaims UserClaims { get; set; }

        public Claim()
        {
            ClaimId = Guid.NewGuid();
        }

        public Claim(string t, string v)
        {
            ClaimId = Guid.NewGuid();
            Title = t;
            Value = v;
        }

        public Claim(ClaimDTO claimDTO)
        {
            ClaimId = Guid.NewGuid();
            Title = claimDTO.Title;
            Value = claimDTO.Value;
        }

        public Boolean Equals(Claim obj)
        {
            if (obj == null)
            {
                return false;
            }

            Claim otherClaim = obj as Claim;

            if (otherClaim != null)
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
