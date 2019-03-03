using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
	public class PasswordSalt
	{
		// Automatic Properties
		[Key]
		[ForeignKey("UserAccount")]
		public int? Id { get; set; }

		public string Salt { get; set; }

		// Navigation Property
		public virtual UserAccount UserAccount { get; set; }

		// Constructors
		public PasswordSalt() { }

		public PasswordSalt(string salt)
		{
			Salt = salt;
		}
}
