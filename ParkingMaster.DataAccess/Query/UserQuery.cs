using ParkingMaster.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;

namespace DataAccess
{
    //queareis for userContext
    /// </summary>
    public class UserQueary
    {
        // Instantiate the User context
        UserContext context = new UserContext();
    
        //get user by email
        public TransferDTO<UserAccount> GetUserByEmail(string email)
        {
            try
            {
                var userAccount = (from account in context.UserAccounts
                                   where account.Username == email
                                   select account).FirstOrDefault();

                // Return a ResponseDto with a UserAccount model
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

       
        //TODO: add the rest of the paramaters to a individual user
        public TransferDTO<bool> StoreIndividualUser(UserAccount userAccount, UserClaims userClaims, StandardUser standardUser, IList<SecurityQuestion> securityQuestions)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Add UserAccount
                    context.UserAccounts.AddOrUpdate(userAccount);
                    context.SaveChanges();

                    // Get (email) UserAccount
                    var userEmail = (from account in context.UserAccounts
                                  where account.Email == userAccount.Email
                                  select account.Id).SingleOrDefault();

                    //TODO: update dependencies
                    userClaims.Id = userEmail;
                    standardUser.Id = userEmail;

                    // Add SecurityQuestions
                    foreach (var securityQuestion in securityQuestions)
                    {
                        securityQuestion.UserId = userEmail;
                        context.SecurityQuestions.Add(securityQuestion);
                        context.SaveChanges();
                    }

                    // Get SecurityQuestions in database
                    var updatedSecurityQuestions = (from question in context.SecurityQuestions
                                                    where question.UserId == userEmail
                                                    select question).ToList();

                   
                    // Add UserClaims
                    context.UserClaims.Add(userClaims);

                    // Add StandardUser
                    context.StandardUser.Add(standardUser);
                    context.SaveChanges();

                    // Commit transaction to database
                    dbContextTransaction.Commit();

                    // Return a true ResponseDto
                    return new ResponseDto<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    // undo if it doesnt work
                    dbContextTransaction.Rollback();
                    // TransferDTO is false
                    return new TransferDTO<bool>()
                    {
                        Data = false,
                       
                    };
                }
            }
        }
    }
}

