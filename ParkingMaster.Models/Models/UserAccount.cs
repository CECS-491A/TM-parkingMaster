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
	[Table("ParkingMaster.UserAccount")]
	public class UserAccount
	{
		// Automatic Properties
		[Key]
		public int? Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool? IsActive { get; set; }
		public bool? IsFirstTimeUser { get; set; }
		public string RoleType { get; set; }

		// Navigation Properties
		public virtual PasswordSalt PasswordSalt { get; set; }
		public virtual AuthenticationToken AuthenticationToken { get; set; }
		public virtual ICollection<SecurityQuestions> SecurityQuestions { get; set; }
		public virtual UserClaims UserClaims { get; set; }

		// Constructors
		public UserAccount() { }

		public UserAccount(string username)
		{
			Username = username;
		}

		public UserAccount(string username, string password, bool isActive, bool isFirstTimeUser, string roleType)
		{
			Username = username;
			Password = password;
			IsActive = isActive;
			IsFirstTimeUser = isFirstTimeUser;
			RoleType = roleType;
		}
	}
}
