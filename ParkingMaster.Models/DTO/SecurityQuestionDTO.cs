using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
	public class SecurityQuestionDTO
	{
		// Automatic Properties
		public int Question { get; set; }
		public string Answer { get; set; }

		// Constructors
		public SecurityQuestionDTO() { }

		public SecurityQuestionDTO(int question)
		{
			Question = question;
		}
	}
}
