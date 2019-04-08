using ParkingMaster.DataAccess;
using ParkingMaster.Models;

namespace ParkingMaster.Validators
{
    //rules to validate user
    public class UserValidator
    {
        // The CheckIfUserExists method.
        // Checks if a user exists in the user data store.
    
        public ResponseDto<bool> CheckIfUserExists(string username)
        {
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.GetUserByUsername(username);
                return new ResponseDto<bool>()
                {
                    // If user exists, then return true
                    Data = gatewayResult.Data != null,
                    Error = gatewayResult.Error
                };
            }
        }

    }
}
