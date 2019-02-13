using System.ComponentModel.DataAnnotations;

namespace parkingMaster
{
    public class EditEmail
    {
       
        [Required]
        public string Email { get; set; } 

        public string NewEmail { get; set; } 

   
    }
}
