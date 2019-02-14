using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingMaster.DataAccess.Models
{
   
    [Table("ParkingMaster.SecurityQuestion")]
    public class SecurityQuestion : SecurityQuestion, Entity
    {
        [Key]
        public int? Id { get; set; }
        [ForeignKey("Email")]
        public string Email { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public virtual SecurityAnswerSalt SecurityAnswerSalt { get; set; }

        // Constructors
        public SecurityQuestion() { }

        public SecurityQuestion(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }
    }
}
