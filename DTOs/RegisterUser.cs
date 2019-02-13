using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace parkingMaster.DTOs
{
    public class RegisterUserDTO
    {
        // Automatic Properties
        [Required]
        public UserAccountDTO UserAccountDTO { get; set; }
        [Required]
        public IList<SecurityQuestionDto> SecurityQuestionDtos { get; set; }
    }
}
