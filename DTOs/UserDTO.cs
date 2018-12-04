namespace DTO
{
    public class UserDTO
    {
        public string email { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }

        //constructors
        public UserDTO(string email, string phone, string city, string state, string country)
        {
            Email = email;
            Phone = phone;
            City = city;
            State = state;
            Country = country; 
   
        }

        public UserDTO() { }
    }

    
    }
}
