using ParkingMaster.Models.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Tests
{
	//Test UserGateway
    [TestClass]
	public class UserGatewayTest
	{
		[TestMethod]
		public void Should_StoreUser_When_ValidModelsArePassedIn()
		{
			// Arrange : initializes objects and sets the value of the data
			//that is passed to the method under test.
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
			
			var userGateway = new UserGateway();

			// Act invokes the method under test with the arranged parameters.
			Action act = () => userGateway.StoreIndividualUser(userAccount, passwordSalt, claims);

			// Assert verifies that the action of the method under test behaves as expected.
			act.Should().NotThrow();
		}

		
	}
}
