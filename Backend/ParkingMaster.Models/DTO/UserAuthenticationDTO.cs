using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
	public class UserAuthenticationDTO
	{
		// Automatic Properties
		public string Username { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }

		// Constructors
		public UserAuthenticationDTO(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
