using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Models
{
    class UserDTO : BaseDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; } // DateTime yyyymmdd
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        //public List<Claim> Claims { get; set; }
        //Security Questions ??


        // Constructor with null values
        public UserDTO()
        {
            Email = Email_NullValue;
            Password = Password_NullValue;
            DateOfBirth = DateOfBirth_NullValue;
            City = City_NullValue;
            State = State_NullValue;
            Country = Country_NullValue;
        }

    }
}
