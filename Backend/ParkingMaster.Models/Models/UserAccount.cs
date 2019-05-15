using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
//For properties relating to a user account
{
	[Table("ParkingMaster.UserAccounts")]
	public class UserAccount
	{
		// Automatic Properties
		[Key]
		public Guid Id { get; set; }
        public Guid SsoId { get; set; }
        [StringLength(350)]
        [Index(IsUnique=true)]
		public string Username { get; set; }
		public bool IsActive { get; set; }
		public bool AcceptedTOS { get; set; }
		public string RoleType { get; set; }

		// Navigation Properties
		public virtual UserClaims UserClaims { get; set; }

		// Constructors
		public UserAccount()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            AcceptedTOS = true;
        }

		public UserAccount(string username)
		{
            Id = Guid.NewGuid();
            IsActive = true;
            AcceptedTOS = true;
            Username = username;
		}

		public UserAccount(Guid ssoId, string username, bool isActive, bool acceptedTOS, string roleType)
		{
            Id = Guid.NewGuid();
            SsoId = ssoId;
            Username = username;
			IsActive = isActive;
            AcceptedTOS = acceptedTOS;
			RoleType = roleType;
		}
	}
}
