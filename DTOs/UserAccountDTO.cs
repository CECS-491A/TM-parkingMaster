using System.ComponentModel.DataAnnotations;

namespace ParkingMaster.DataAccess.Models
{

    public class UserAccountDTO : UserAccount
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Registered { get; set; }
        public bool? NewUser { get; set; }
        public string Role { get; set; }

        // Constructors
        public UserAccountDto()
        {
            Username = "";
            Password = "";
            Role = "";
        }
        public UserAccountDto(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public UserAccountDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
