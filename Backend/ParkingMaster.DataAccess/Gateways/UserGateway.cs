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
		UserContext context = new UserContext();

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
		public ResponseDTO<bool> StoreIndividualUser(UserAccount userAccount, PasswordSalt passwordSalt, UserClaims userClaims)
		{
			using (var dbContextTransaction = context.Database.BeginTransaction())
			{
				try
				{
					// Add UserAccount
					context.UserAccounts.AddOrUpdate(userAccount);
					context.SaveChanges();

					// Get Id from UserAccount
					var userId = (from account in context.UserAccounts
								  where account.Username == userAccount.Username
								  select account.Id).SingleOrDefault();

					// Set UserId to dependencies
					userClaims.Id = userId;

					// Add PasswordSalt
					context.PasswordSalts.AddOrUpdate(passwordSalt);

					// Add UserClaims
					context.UserClaims.Add(userClaims);


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

					// PasswordSalt
					// Queries for the password salt based on user account id.
					var userPasswordSalt = (from passwordSalt in context.PasswordSalts
											where passwordSalt.Id == userAccount.Id
											select passwordSalt).FirstOrDefault();

					// Checks if user password salt is null, if not then delete from database.
					if (userPasswordSalt != null)
					{
						context.PasswordSalts.Remove(userPasswordSalt);
					}

					// User Claims
					// Queries for the users claims based on user account id and claims user id.
					var userClaims = (from claims in context.UserClaims
									  where claims.Id == userAccount.Id
									  select claims).FirstOrDefault();

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



		//Password reset
		public ResponseDTO<bool> ResetPassword(string username, string newPassword)
		{
			using (var dbContextTransaction = context.Database.BeginTransaction())
			{
				try
				{
					//Queries for the user account based on the username passed in.
					var userAccount = (from account in context.UserAccounts
									   where account.Username == username
									   select account).SingleOrDefault();

					//Select the password from useraccount and give it the new username.
					userAccount.Password = newPassword;
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

		//Update PW
		public ResponseDTO<bool> UpdatePassword(UserAccount userAccount, PasswordSalt passwordSalt)
		{
			using (var dbContextTransaction = context.Database.BeginTransaction())
			{
				try
				{

					// Get Id from username
					userAccount.Id = (from account in context.UserAccounts
									  where account.Username == userAccount.Username
									  select account.Id).FirstOrDefault();
					context.SaveChanges();

					// Set UserId to dependencies
					passwordSalt.Id = userAccount.Id;
					context.SaveChanges();

					// Update UserAccount
					context.UserAccounts.AddOrUpdate(userAccount);

					// Update PasswordSalt
					context.PasswordSalts.AddOrUpdate(passwordSalt);
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

		/// <summary>
		/// Dispose of the context
		/// </summary>
		void IDisposable.Dispose()
		{
			context.Dispose();
		}
	}
}