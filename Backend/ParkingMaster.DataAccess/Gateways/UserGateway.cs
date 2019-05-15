using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.Constants;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess
{

	// Methods used to communicate with user context

	public class UserGateway : IDisposable
	{
		// Open the User context
		UserContext context;

        public UserGateway()
        {
            context = new UserContext();
        }

        public UserGateway(UserContext c)
        {
            context = c;
        }

        /// <summary>
        /// The GetUserByUsername method.
        /// Gets a user by username
        public ResponseDTO<UserAccountDTO> GetUserByUsername(string username)
		{
			try
			{
				var userAccount = (from account in context.UserAccounts
								   where account.Username == username
								   select account).First();

				// Return a ResponseDto with a UserAccount model
				return new ResponseDTO<UserAccountDTO>()
				{
					Data = new UserAccountDTO(userAccount)
                };

			}
			catch (Exception)
			{
				return new ResponseDTO<UserAccountDTO>()
				{
					Data = null,
                    Error = ErrorStrings.DATA_ACCESS_ERROR
                };
			}
		}
        public ResponseDTO<UserAccountDTO> GetUserBySsoId(Guid id)
        {
            try
            {
                var userAccount = (from account in context.UserAccounts
                                   where account.SsoId == id
                                   select account).First();

                // Return a ResponseDto with a UserAccount model
                return new ResponseDTO<UserAccountDTO>()
                {
                    Data = new UserAccountDTO(userAccount)
                };
            }
            catch (Exception)
            {
                return new ResponseDTO<UserAccountDTO>()
                {
                    Data = null,
                    Error = ErrorStrings.DATA_ACCESS_ERROR
                };
            }
        }

        public ResponseDTO<UserAccountDTO> GetUserByUserId(Guid id)
        {
            try
            {
                var userAccount = (from account in context.UserAccounts
                                   where account.Id == id
                                   select account).First();

                // Return a ResponseDto with a UserAccount model
                return new ResponseDTO<UserAccountDTO>()
                {
                    Data = new UserAccountDTO(userAccount)
                };
            }
            catch (Exception)
            {
                return new ResponseDTO<UserAccountDTO>()
                {
                    Data = null,
                    Error = ErrorStrings.DATA_ACCESS_ERROR
                };
            }
        }

        //Store a user
        public ResponseDTO<bool> StoreIndividualUser(UserAccount userAccount, List<Claim> claims)
		{
			using (var dbContextTransaction = context.Database.BeginTransaction())
			{
				try
				{

                    // Add UserAccount
                    context.UserAccounts.Add(userAccount);

                    // Add UserClaims
                    context.UserClaims.Add(new UserClaims(userAccount.Id, claims));

					context.SaveChanges();

					// Commit transaction to database
					dbContextTransaction.Commit();

					// Return a true ResponseDto
					return new ResponseDTO<bool>()
					{
						Data = true
					};
				}
				catch (Exception)
				{
					// Rolls back the changes saved in the transaction
					dbContextTransaction.Rollback();
					// Returns a false ResponseDto
					return new ResponseDTO<bool>()
					{
						Data = false,
                        Error = ErrorStrings.DATA_ACCESS_ERROR
                    };
				}
			}
		}




		//deactivate user
		public ResponseDTO<bool> DeactivateUser(string username)
		{
			using (var dbContextTransaction = context.Database.BeginTransaction())
			{
				try
				{
					//Queries for the user account based on username.
					var userAccount = (from account in context.UserAccounts
									   where account.Username == username
									   select account).FirstOrDefault();

					userAccount.IsActive = false;
					context.SaveChanges();
					dbContextTransaction.Commit();

					return new ResponseDTO<bool>()
					{
						Data = true
					};
				}
				catch (Exception)
				{
					dbContextTransaction.Rollback();

					return new ResponseDTO<bool>()
					{
						Data = false,
                        Error = ErrorStrings.DATA_ACCESS_ERROR

                    };
				}
			}
		}

		//reactivate user
		public ResponseDTO<bool> ReactivateUser(string username)
		{
			using (var dbContextTransaction = context.Database.BeginTransaction())
			{
				try
				{
					//Queries for the user account based on username.
					var activeStatus = (from account in context.UserAccounts
										where account.Username == username
										select account).SingleOrDefault();

					activeStatus.IsActive = true;
					context.SaveChanges();
					dbContextTransaction.Commit();

					return new ResponseDTO<bool>()
					{
						Data = true
					};
				}
				catch (Exception)
				{
					dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = ErrorStrings.DATA_ACCESS_ERROR

                    };
				}
			}
		}

        public ResponseDTO<bool> AcceptTOS(Guid id)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    //Queries for the user account based on username.
                    var user = (from account in context.UserAccounts
                                        where account.Id == id
                                        select account).SingleOrDefault();

                    user.AcceptedTOS = true;
                    context.SaveChanges();
                    dbContextTransaction.Commit();

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = ErrorStrings.DATA_ACCESS_ERROR

                    };
                }
            }
        }

        //Delete user by username
        public ResponseDTO<bool> DeleteUser(Guid userId)
		{
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
				try
				{

					// Queries for the user account based on username.
					var userAccount = (from account in context.UserAccounts
									   where account.Id == userId
                                       select account).FirstOrDefault();

					// Checking if user account is null.
					if (userAccount == null)
					{
						return new ResponseDTO<bool>()
						{
							Data = false,
						};
					}


					// User Claims
					// Queries for the users claims based on user account id and claims user id.
					var userClaims = (from claims in context.UserClaims
									  where claims.Id == userAccount.Id
									  select claims).FirstOrDefault();

                    var userClaimsId = userClaims.Id;

					// Checks if claims result is null, if not then delete from database.
					if (userClaims != null)
					{
						context.UserClaims.Remove(userClaims);
					}

					//Delete useraccount
					context.UserAccounts.Remove(userAccount);
					context.SaveChanges();
					dbContextTransaction.Commit();

					return new ResponseDTO<bool>()
					{
						Data = true
					};
				}
				catch (Exception)
				{
					dbContextTransaction.Rollback();
                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = ErrorStrings.DATA_ACCESS_ERROR
                    };
				};
			}
		}


        // Returns all User Accounts in the data store
        public ResponseDTO<List<UserAccount>> GetAllUserAccounts()
        {
            ResponseDTO<List<UserAccount>> response = new ResponseDTO<List<UserAccount>>();
            try
            {
                response.Data = context.UserAccounts.ToList<UserAccount>();
                return response;
            }
            catch
            {
                response.Data = null;
                response.Error = ErrorStrings.DATA_ACCESS_ERROR;
                return response;
            }
        }

        public ResponseDTO<List<ClaimDTO>> GetUserClaims(string username)
        {
            ResponseDTO<List<ClaimDTO>> response = new ResponseDTO<List<ClaimDTO>>
            {
                Data = new List<ClaimDTO>()
            };
            List<Claim> claimsList = new List<Claim>();
            try
            {
                // Queries for the user account based on username.
                var userAccount = (from account in context.UserAccounts
                                   where account.Username == username
                                   select account).FirstOrDefault();

                if(userAccount == null)
                {
                    response.Data = null;
                    response.Error = ErrorStrings.USER_NOT_FOUND;
                    return response;
                }

                claimsList = (from claims in context.Claims
                                 where claims.UserClaimsId == userAccount.Id
                                 select claims).ToList<Claim>();

                //Transform List of Claims into a List of ClaimDTOs
                foreach(Claim claim in claimsList)
                {
                    response.Data.Add(new ClaimDTO(claim.Title, claim.Value));
                }

                return response;
            }
            catch
            {
                response.Data = null;
                response.Error = ErrorStrings.DATA_ACCESS_ERROR;
                return response;
            }

        }

        public ResponseDTO<List<ClaimDTO>> GetUserClaims(Guid userId)
        {
            ResponseDTO<List<ClaimDTO>> response = new ResponseDTO<List<ClaimDTO>>
            {
                Data = new List<ClaimDTO>()
            };
            List<Claim> claimsList = new List<Claim>();
            try
            {

                claimsList = (from claims in context.Claims
                              where claims.UserClaimsId == userId
                              select claims).ToList<Claim>();

                //Transform List of Claims into a List of ClaimDTOs
                foreach (Claim claim in claimsList)
                {
                    response.Data.Add(new ClaimDTO(claim.Title, claim.Value));
                }

                return response;
            }
            catch
            {
                response.Data = null;
                response.Error = ErrorStrings.DATA_ACCESS_ERROR;
                return response;
            }

        }

        public ResponseDTO<UserAccountDTO> SetRole(UserAccountDTO newAccountInfo)
        {
            ResponseDTO<UserAccountDTO> response = new ResponseDTO<UserAccountDTO>();
            
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    var currentUser = (from users in context.UserAccounts
                                         where users.Id == newAccountInfo.Id
                                         select users).FirstOrDefault();

                    currentUser.RoleType = newAccountInfo.RoleType;
                    context.SaveChanges();
                    dbContextTransaction.Commit();

                    response.Data = new UserAccountDTO(currentUser);

                    return response;
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    response.Data = null;
                    response.Error = ErrorStrings.DATA_ACCESS_ERROR;
                    return response;
                }
            }

        }

        public ResponseDTO<bool> ResetUserClaims (Guid userId, ICollection<Claim> newClaims)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();

            UserClaims currentUserClaims;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    currentUserClaims = (from userClaims in context.UserClaims
                                         where userClaims.Id == userId
                                         select userClaims).FirstOrDefault();


                    context.UserClaims.Remove(currentUserClaims);
                    context.SaveChanges();
                    context.UserClaims.Add(new UserClaims(userId, newClaims));
                    context.SaveChanges();
                    dbContextTransaction.Commit();

                    response.Data = true;
                    return response;
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    response.Data = false;
                    response.Error = ErrorStrings.DATA_ACCESS_ERROR;
                    return response;
                }
            }

        }

        //Deletes all users to reinitialize the database
        public void ResetDatabase()
        {

            List<UserAccount> userAccounts = GetAllUserAccounts().Data;

            foreach(UserAccount acc in userAccounts)
            {
                DeleteUser(acc.Id);
            }

        }


		/// <summary>
		/// Dispose of the context
		/// </summary>
		void IDisposable.Dispose()
		{
			context.Dispose();
		}
	}
}