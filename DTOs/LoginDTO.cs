namespace 
{
   
    public class LoginDto
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        // Constructors
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
