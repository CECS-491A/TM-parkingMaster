using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
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
        public ResponseDTO<UserAccount> GetUserByUsername(string username)
		{
			try
			{
				var userAccount = (from account in context.UserAccounts
								   where account.Username == username
								   select account).FirstOrDefault();

				// Return a ResponseDto with a UserAccount model
				return new ResponseDTO<UserAccount>()
				{
					Data = userAccount
				};
			}
			catch (Exception)
			{
				return new ResponseDTO<UserAccount>()
				{
					Data = new UserAccount(username),
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

					};
				}
			}
		}

		//Delete user by username
		public ResponseDTO<bool> DeleteUser(string username)
		{
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
				try
				{

					// Queries for the user account based on username.
					var userAccount = (from account in context.UserAccounts
									   where account.Username == username
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

					};
				};
			}
		}

        public void ResetDatabase()
        {

            List<UserAccount> userAccounts = context.UserAccounts.ToList<UserAccount>();

            foreach(UserAccount acc in userAccounts)
            {
                DeleteUser(acc.Username);
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