using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
	public class EditUserDTO
	{
		[Required]
		public string Username { get; set; } //The username that will be edited. 

		public string NewUsername { get; set; } //Part of user account 

		public string NewDisplayName { get; set; } //Part of user account 
	}
}
