using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
	public class UserAccountDTO
	{
		// Automatic Properties
		[Required]
		public string Username { get; set; }
		public string Password { get; set; }
		public bool? IsActive { get; set; }
		public bool? IsFirstTimeUser { get; set; }
		public string RoleType { get; set; }

		// Constructors
		public UserAccountDTO()
		{
			Username = "";
			Password = "";
			RoleType = "";
		}
		public UserAccountDTO(string username, string password, string roleType)
		{
			Username = username;
			Password = password;
			RoleType = roleType;
		}

		public UserAccountDTO(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
