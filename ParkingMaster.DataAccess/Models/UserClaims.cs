using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ParkingMaster.DataAccess.Models
{
    
    [Table("ParkingMaster.UserClaims")]
    public class UserClaims : Entity
    {
      
        [Key]
        [ForeignKey("UserAccount")]
        public int? Id { get; set; }
        [NotMapped]
        public ICollection<Claim> Claims { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }

        // Constructors
        public UserClaims() { }

        public UserClaims(ICollection<Claim> claims)
        {
            Claims = claims;
        }
    }
}
