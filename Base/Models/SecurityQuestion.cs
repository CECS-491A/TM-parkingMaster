using System.ComponentModel.DataAnnotations;

namespace Base
{
    // properties for security questions
    [Table("SecurityQuestion")]
    public class SecurityQuestion
    {
        // Automatic Properties
       
        [ForeignKey("UserAccount")]
        public string Email { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        //TODO: add maybe Navigation Properties
        public virtual UserAccount UserAccount { get; set; }
      

        // Constructors
        public SecurityQuestion() { }

        public SecurityQuestion(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }
    }
}
