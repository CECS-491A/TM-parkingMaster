using ParkingMaster.Models;
namespace ParkingMaster.DataAccess
{
    /// methods to communicate with UserContext
    public class UserGateway
    {
        // Open the User context
        UserContext context = new UserContext();

        //get a user by email
        public ResponseDto<UserAccount> GetUserByEmail(string email)
        {
            try
            {
                var someUser = (from account in context.UserAccounts
                                   where account.Username == email
                                   select account).FirstOrDefault();

                // Return a ResponseDto with a UserAccount model
                return new ResponseDto<UserAccount>()
                {
                    Data = userAccount
                };
            }
            catch (Exception)
            {
                return new ResponseDto<UserAccount>()
                {
                    Data = new UserAccount(email),
                    
                };
            }
        }
    }
}
