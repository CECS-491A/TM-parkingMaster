using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.Models
{
	[Table("ParkingMaster.SecurityQuestions")]
	public class SecurityQuestions
	{
		// Automatic PropertSSies
		[Key]
		public int? Id { get; set; }
		[ForeignKey("UserAccount")]
		public int? UserId { get; set; }
		public int Question { get; set; }
		public string Answer { get; set; }

		// Navigation Properties
		public virtual UserAccount UserAccount { get; set; }
		public virtual SecurityAnswerSalt SecurityAnswerSalt { get; set; }

		// Constructors
		public SecurityQuestions() { }

		public SecurityQuestions(int question, string answer)
		{
			Question = question;
			Answer = answer;
		}
	}
}