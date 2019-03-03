using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
	public class ResetPasswordDTO
	{
		// Automatic properties
		[Required]
		public string Username { get; set; }
		public string Password { get; set; }
		public IList<SecurityQuestionDTO> SecurityQuestionDTO { get; set; }

		// Constructors
		public ResetPasswordDTO() { }
		public ResetPasswordDTO(string username)
		{
			Username = username;
		}
		public ResetPasswordDTO(string username, IList<SecurityQuestionDTO> securityQuestionDTO)
		{
			Username = username;
			SecurityQuestionDTO = securityQuestionDTO;
		}
		public ResetPasswordDTO(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
