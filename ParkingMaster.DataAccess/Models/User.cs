using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingMaster.DataAccess.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; } // DateTime yyyymmdd
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public Boolean Activated { get; set; }
        public ICollection<Claim> UserClaims { get; set; }

        // Constructor with null values
        public User()
        {
            Email = null;
            Password = null;
            DateOfBirth = null;
            City = null;
            State = null;
            Country = null;
            Role = null;
            Activated = false;
            UserClaims = new List<Claim>();
        }

    }
}
