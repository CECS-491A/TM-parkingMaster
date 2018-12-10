using System.Collections.Generic;

namespace Base
{
    // Properties for user account
    //https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database


    [Table("UserAccount")]
    public class UserAccount
    {
        //Properties
        [Key]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        //TODO: Add Navigational Properties


        // Constructors
        public UserAccount() { }

        public UserAccount(string email, string phone, string city, string state, string country, string password, string role)
        {
            Email = email;
            Phone = phone;
            City = city;
            State = state;
            Country = country;
            Password = password;
            Role = role;
        }
    }
}
