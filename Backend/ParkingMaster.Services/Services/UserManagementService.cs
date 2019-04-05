using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Services.Services
{
    public class UserManagementService : IUserManagementService
    {

        private UserGateway _userGateway;

        public UserManagementService(UserGateway userGateway)
        {
            _userGateway = userGateway;
        }

        public UserManagementService()
        {
            _userGateway = new UserGateway();
        }

        public ResponseDTO<bool> CreateUser(UserAccount user, List<Claim> claims)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();

            if (user == null)
            {
                response.Data = false;
                response.Error = "Attempted to store Null UserAccount.";
                return response;
            }
            if (claims == null)
            {
                response.Data = false;
                response.Error = "Attempted to store Null UserClaims.";
                return response;
            }
            try
            {
                response = _userGateway.StoreIndividualUser(user, claims);
                return response;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public ResponseDTO<bool> DeleteUser(UserAccount user)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();

            if (user == null)
            {
                response.Data = false;
                response.Error = "Attempted to delete Null UserAccount.";
                return response;
            }
            try
            {
                response = _userGateway.DeleteUser(user.Username);
                return response;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public ResponseDTO<bool> DeleteUser(string username)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();

            if (username == null)
            {
                response.Data = false;
                response.Error = "Attempted to delete Null username.";
                return response;
            }
            try
            {
                response = _userGateway.DeleteUser(username);
                return response;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public ResponseDTO<bool> UpdateUser(UserAccount user)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<UserAccount> GetUserByUsername(string username)
        {
            ResponseDTO<UserAccount> response = new ResponseDTO<UserAccount>();

            if (username == null)
            {
                response.Data = null;
                response.Error = "Attempted to delete Null username.";
                return response;
            }
            try
            {
                response = _userGateway.GetUserByUsername(username);
                return response;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public ResponseDTO<UserAccount> GetUserBySsoId(Guid id)
        {
            ResponseDTO<UserAccount> response = new ResponseDTO<UserAccount>();

            if (id == null)
            {
                response.Data = null;
                response.Error = "Attempted to delete Null username.";
                return response;
            }
            try
            {
                response = _userGateway.GetUserBySsoId(id);
                return response;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public ResponseDTO<List<UserAccount>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void AddUserClaim(UserAccount user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<List<ClaimDTO>> GetAllUserClaims(string username)
        {
            ResponseDTO<List<ClaimDTO>> response = new ResponseDTO<List<ClaimDTO>>();
            response = _userGateway.GetUserClaims(username);

            return response;
        }
    }
}
