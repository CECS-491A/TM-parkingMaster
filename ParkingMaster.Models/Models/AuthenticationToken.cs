using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
	public class AuthenticationToken
	{
		// Automatic properties
		[Key]
		[ForeignKey("UserAccount")]
		public int? Id { get; set; }
		public string Username { get; set; }
		public DateTime ExpiresOn { get; set; }
		public string Salt { get; set; }
		public string TokenString { get; set; }

		// Navigation Properties
		public virtual UserAccount UserAccount { get; set; }

		// Constructors
		public AuthenticationToken() { }

		public AuthenticationToken(string username, DateTime expiresOn, string tokenString)
		{
			Username = username;
			ExpiresOn = expiresOn;
			TokenString = tokenString;
		}

		public AuthenticationToken(int? id, string username, DateTime expiresOn, string tokenString)
		{
			Id = id;
			Username = username;
			ExpiresOn = expiresOn;
			TokenString = tokenString;
		}
	}
}
