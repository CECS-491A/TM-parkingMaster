using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
	[Table("ParkingMaster.SecurityAnswerSalt")]
	public class SecurityAnswerSalt
	{
		// Automatic Properties
		[Key]
		[ForeignKey("SecurityQuestion")]
		public int? Id { get; set; }
		public string Salt { get; set; }

	}
}
