﻿using System;
using ParkingMaster.Models.DTO;
using ParkingMaster.Services.Services;
using System.Net;

namespace ParkingMaster.Manager.Managers
{
    public class UserManagementManager
    {
        
        private UserManagementService _userManagementService;

        public UserManagementManager()
        {
            _userManagementService = new UserManagementService();
        }
        /*
        // I am considering moving context.SaveChanges() here, but maybe later

        public bool CreateUser(string email, string password, string dob, string city, string state, string country, string role, bool act)
        {
            try
            {
                User user = new User
                {
                    Email = email,
                    Password = password,
                    DateOfBirth = dob,
                    City = city,
                    State = state,
                    Country = country,
                    Role = role,
                    Activated = act
                };
                _userManagementService.CreateUser(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        */
        public ResponseDTO<bool> DeleteUser(Guid userId)
        {
            try
            {
                return _userManagementService.DeleteUser(userId);
            }
            catch (Exception e)
            {
               return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = "Failed to delete userID: " + userId + "\n" + e.Message
                };
            }
        }

        // Delete User From SsoRequest
        public ResponseDTO<HttpStatusCode> DeleteUser(SsoUserRequestDTO request)
        {
            ResponseDTO<HttpStatusCode> response = new ResponseDTO<HttpStatusCode>();
            ITokenService tokenService = new TokenService();
            if(!tokenService.isValidSignature(request.GetStringToSign(), request.Signature))
            {
                response.Data = (HttpStatusCode)400;
                response.Error = "Signature not valid";
                return response;
            }


            // Check if request id is in guid format
            Guid ssoId;
            try
            {
                ssoId = new Guid(request.SsoId);
            }
            catch (Exception e)
            {
                response.Data = (HttpStatusCode) 400;
                response.Error = "SsoId provided was invalid";
                return response;
            }

            // TODO: Check signature

            UserAccountDTO userAccount;
            ResponseDTO<UserAccountDTO> userAccountResponse = _userManagementService.GetUserBySsoId(ssoId);
            if(userAccountResponse.Data == null)
            {
                // TODO: Add a check if user did not exist or if it was a standard EntityFramework Error
                response.Data = (HttpStatusCode) 404;
                response.Error = "Unable to find ssoId";
                return response;
            }
            else
            {
                userAccount = userAccountResponse.Data;
            }

            ResponseDTO<bool> boolResponse;
            try
            {
                boolResponse = _userManagementService.DeleteUser(userAccount.Id);
            }
            catch (Exception e)
            {
                response.Data = (HttpStatusCode) 500;
                response.Error = "Failed to delete userID: " + userAccount.Id + "\n" + e.Message;
                return response;
            }

            if (boolResponse.Data)
            {
                response.Data = (HttpStatusCode) 200;
                return response;
            }
            else
            {
                response.Data = (HttpStatusCode) 500;
                response.Error = boolResponse.Error;
                return response;
            }
        }

        public ResponseDTO<UserAccountDTO> GetUserBySsoId(Guid ssoId)
        {
            try
            {
                return _userManagementService.GetUserBySsoId(ssoId);
            }
            catch (Exception e)
            {
                return new ResponseDTO<UserAccountDTO>()
                {
                    Data = null,
                    Error = "Failed to get ssoId: " + ssoId + "\n" + e.Message
                };
            }
        }
        /*
        public bool UpdateUser(User user)
        {
            try
            {
                _userManagementService.UpdateUser(user);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userManagementService.GetAllUsers();
        }

    */
    }
}