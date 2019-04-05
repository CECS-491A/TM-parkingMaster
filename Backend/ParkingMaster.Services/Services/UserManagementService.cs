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
                response = _userGateway.DeleteUser(user.Id);
                return response;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //return false;
                throw e;
            }
        }

        public ResponseDTO<bool> DeleteUser(Guid userId)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();

            if (userId == null)
            {
                response.Data = false;
                response.Error = "Attempted to delete Null username.";
                return response;
            }
            try
            {
                response = _userGateway.DeleteUser(userId);
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

        public ResponseDTO<UserAccountDTO> GetUserByUsername(string username)
        {
            ResponseDTO<UserAccountDTO> response = new ResponseDTO<UserAccountDTO>();

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

        public ResponseDTO<UserAccountDTO> GetUserBySsoId(Guid id)
        {
            ResponseDTO<UserAccountDTO> response = new ResponseDTO<UserAccountDTO>();

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
                response.Data = null;
                response.Error = "Error occurred when searching for SssoId: " + id + "\n" + e.Message;
                return response;
            }
        }

        public ResponseDTO<List<UserAccountDTO>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void AddUserClaim(UserAccountDTO user, Claim claim)
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
