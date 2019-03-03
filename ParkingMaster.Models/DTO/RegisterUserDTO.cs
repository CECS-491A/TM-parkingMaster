using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
	public class RegisterUserDTO
	{
		// Automatic Properties
		[Required]
		public UserAccountDTO UserAccountDTO { get; set; }
		[Required]
		public IList<SecurityQuestionDTO> SecurityQuestionDTO{ get; set; }
		[Required]
		public UserProfileDTO UserProfileDTO { get; set; }
	}
}
