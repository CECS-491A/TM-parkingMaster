using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
	[Table("ParkingMaster.UserClaims")]
	public class UserClaims
	{
		// Automatic Properties
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
