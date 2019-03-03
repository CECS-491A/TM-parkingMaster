using ParkingMaster.Models.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using Xunit;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Tests
{
	//Test UserGateway
	public class UserGatewayTest
	{
		[Fact]
		public void Should_StoreUser_When_ValidModelsArePassedIn()
		{
			// Arrange
			var userAccount = new UserAccount()
			{
				Username = "harditsingh",
				Password = "Sup3rs3curePw",
				IsActive = true,
				IsFirstTimeUser = false
			};
			var passwordSalt = new PasswordSalt()
			{
				Salt = "(*&(^RANDOM()*#$"
			};
			var claims = new UserClaims()
			{
				Claims = new Collection<Claim>()
				{
					new Claim("ReadIndividualProfile", "True"),
					new Claim("UpdateIndividualProfile", "True"),
				}
			};
			var securityQuestions = new List<SecurityQuestions()
			{
				new SecurityQuestions()
				{
					Question = 1,
					Answer = "answer1",
				},
				new SecurityQuestions()
				{
					Question = 2,
					Answer = "answer2",
				},
				new SecurityQuestions()
				{
					Question = 3,
					Answer = "answer3",
				}
			};
			var securityAnswerSalts = new List<SecurityAnswerSalt>()
			{
				new SecurityAnswerSalt()
				{
					Salt = "!Q2w#E4r"
				},
				new SecurityAnswerSalt()
				{
					Salt = "%T6y&U8i"
				},
				new SecurityAnswerSalt()
				{
					Salt = "&U8i(O0p"
				}
			};
			
			var userGateway = new UserGateway();

			// Act
			Action act = () => userGateway.StoreIndividualUser(userAccount, passwordSalt, claims, userProfile, securityQuestions, securityAnswerSalts);

			// Assert
			act.Should().NotThrow();
		}

		
	}
}
