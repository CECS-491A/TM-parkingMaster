using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
	class Configuration : DbMigrationsConfiguration<ParkingMaster.DataAccess.UserContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			MigrationsDirectory = @"Migrations\UserDbContext";
		}

		//Seed method
		protected override void Seed(ParkingMaster.DataAccess.UserContext context)
		{
			

			

			
			// maxNumberOfUsers must be an even number
			const int maxUsers = 50;
			

			 
			// Instantiate Randomizer
			var randomizer = new Random();

			
			// Seeding users to the database where half are standard users
			for (var i = 1; i <= maxUsers; i++)
			{
				// AddorUpdate to UserAccount table
				context.UserAccounts.AddOrUpdate
				(
					new UserAccount()
					{
						Id = i,
						Username = $"username{i}",
						Password = $"password{i}",
						IsActive = true,
						IsFirstTimeUser = false,
						RoleType = "standard"
					}
				);
				context.SaveChanges();

				
				// AddorUpdate to PasswordSalt table
				context.PasswordSalts.AddOrUpdate
				(
					new PasswordSalt()
					{
						Id = i,
						Salt = $"salt{i}"
					}
				);

				for (var j = 1 + 3 * (i - 1); j <= 3 + 3 * (i - 1); j++)
				{
					// AddorUpdate to SecurityQuestion table
					context.SecurityQuestions.AddOrUpdate
					(
						new SecurityQuestions()
						{
							Id = j,
							UserId = i,
							//9 q's for now
							Question = randomizer.Next(1, 9 + 1),
							Answer = $"answer{j}"
						}
					);

					// AddorUpdate to SecurityAnswerSalt table
					context.SecurityAnswerSalts.AddOrUpdate
					(
						new SecurityAnswerSalt()
						{
							Id = j,
							Salt = $"salt{j}"
						}
					);
				}
				context.SaveChanges();

				/*
				// Creating a list of claims
				var claims = new List<Claim>
				{
					TODO: Still working on this
				};

				*/

				/*
				// AddorUpdate to UserClaims table
				context.UserClaims.AddOrUpdate
				(
					TODO:
					}
				);
				*/


			}
		}
	}
}
