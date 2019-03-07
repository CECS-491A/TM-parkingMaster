using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingMaster.DataAccess.Models
{
    
    [Table("ParkingMaster.UserAccount")]
    public class UserAccount : UserAccount, Entity
    {

        [Key]
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Registered { get; set; }
        public bool? NewUser { get; set; }
        public string Role { get; set; }

        public virtual PasswordSalt PasswordSalt { get; set; }
        public virtual ICollection<SecurityQuestion> SecurityQuestions { get; set; }
        public virtual UserClaims UserClaims { get; set; }
      
        // Constructors
        public UserAccount() { }

        public UserAccount(string email)
        {
            Email = email;
        }

        public UserAccount(string email, string password, bool registered, bool newUser, string role)
        {
            Email = email;
            Password = password;
            Registered = registered;
            NewUser = newUser;
            Role = role;
        }
    }
}
