using ParkingMaster.Models;
namespace ParkingMaster.DataAccess
{
    /// methods to communicate with UserContext
    public class UserGateway
    {
        // Open the User context
        UserContext context = new UserContext();

        //get a user by email
        public TransferDTO<UserAccount> GetUserByEmail(string email)
        {
            try
            {
                var someUser = (from account in context.UserAccounts
                                   where account.Username == email
                                   select account).FirstOrDefault();

                // Return a TransferDTO with a UserAccount model
                return new TransferDTO<UserAccount>()
                {
                    Data = userAccount
                };
            }
            catch (Exception)
            {
                return new TransferDTO<UserAccount>()
                {
                    Data = new UserAccount(email),
                
                };
            }
        }
    }
}
