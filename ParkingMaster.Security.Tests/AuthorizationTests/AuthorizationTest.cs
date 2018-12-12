using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.Security.Authorization;

namespace ParkingMaster.Security.Tests.AuthorizationTests
{
    [TestClass]
    public class AuthorizationTest
    {

        private DatabaseContext _dbContext;

        private static List<Claim> GetUserOneClaims()
        {
            List<Claim> userClaims = new List<Claim>();

            userClaims.Add(new Claim("Action", "Logout"));
            userClaims.Add(new Claim("User", "pnguyen@gmail.com"));
            userClaims.Add(new Claim("Client", "client1@yahoo.com"));

            return userClaims;
        }

        private static List<Claim> GetUserTwoClaims()
        {
            List<Claim> userClaims = new List<Claim>();

            userClaims.Add(new Claim("Action", "Logout"));
            userClaims.Add(new Claim("User", "tnguyen@gmail.com"));
            userClaims.Add(new Claim("Client", "client2@gmail.com"));

            return userClaims;
        }

        private static List<Claim> GetClientOneClaims()
        {
            List<Claim> clientClaims = new List<Claim>();

            clientClaims.Add(new Claim("Action", "Logout"));
            clientClaims.Add(new Claim("User", "client1@gmail.com"));

            return clientClaims;
        }

        private static List<Claim> GetClientTwoClaims()
        {
            List<Claim> clientClaims = new List<Claim>();

            clientClaims.Add(new Claim("Action", "Logout"));
            clientClaims.Add(new Claim("User", "client2@gmail.com"));

            return clientClaims;
        }

        private static IEnumerable<object[]> GetPassData()
        {

            yield return new object[] { "User 1 attempts to logout.  ", GetUserOneClaims(), new Claim("Action", "Logout") };
            yield return new object[] { "User 2 attempts to logout.  ", GetUserTwoClaims(), new Claim("Action", "Logout") };
            yield return new object[] { "Client 1 attempts to logout.  ", GetClientOneClaims(), new Claim("Action", "Logout") };
            yield return new object[] { "Client 2 attempts to logout.  ", GetClientTwoClaims(), new Claim("Action", "Logout") };

            yield return new object[] { "User 1 attempts to create other user.  ", GetUserOneClaims(), new Claim("Action", "CreateOtherUser") };
            yield return new object[] { "Client 1 attempts to create other user.  ", GetClientOneClaims(), new Claim("Action", "CreateOtherUser") };
            yield return new object[] { "Client 2 attempts to create other user.  ", GetClientTwoClaims(), new Claim("Action", "CreateOtherUser") };

            yield return new object[] { "User 2 attempts client 2 action.  ", GetUserTwoClaims(), new Claim("Action", "Client2Action") };
            yield return new object[] { "Client 2 attempts client 2 action.  ", GetClientTwoClaims(), new Claim("Action", "Client2Action") };

        }

        private static IEnumerable<object[]> GetFailData()
        {

            yield return new object[] { "User 2 attempts to create other user.  ", GetUserTwoClaims(), new Claim("Action", "CreateOtherUser") };

            yield return new object[] { "User 1 attempts client 1 action.  ", GetUserOneClaims(), new Claim("Action", "Client2Action") };
            yield return new object[] { "Client 1 attempts client 1 action.  ", GetClientOneClaims(), new Claim("Action", "Client2Action") };

            yield return new object[] { "User 1 attempts disabled action.  ", GetUserOneClaims(), new Claim("Action", "DisabledAction") };
            yield return new object[] { "User 2 attempts disabled action.  ", GetUserTwoClaims(), new Claim("Action", "DisabledAction") };
            yield return new object[] { "Client 1 attempts disabled action.  ", GetClientOneClaims(), new Claim("Action", "DisabledAction") };
            yield return new object[] { "Client 2 attempts disabled action.  ", GetClientTwoClaims(), new Claim("Action", "DisabledAction") };
        }

        private static IEnumerable<object[]> GetNullData()
        {

            yield return new object[] { "All null inputs.  ", null, null };

            yield return new object[] { "User claims null with valid function.  ", null, new Claim("Action", "Client2Action") };
            yield return new object[] { "Valid user claims with null function.  ", GetClientOneClaims(), null };
        }



        [DataTestMethod]
        [DynamicData(nameof(GetPassData), DynamicDataSourceType.Method)]
        public void Authorize_ValidInputs_ReturnsTrue(string testTitle, List<Claim> userClaims, Claim functionClaim)
        {
            // Arrange
            Boolean expected = true;
            Boolean actual = false;
            using(_dbContext = new DatabaseContext())
            {
                AuthorizationClient authClient = new AuthorizationClient(_dbContext);

                // Act
                actual = authClient.Authorize(userClaims, functionClaim);
            }

            

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [DataTestMethod]
        [DynamicData(nameof(GetFailData), DynamicDataSourceType.Method)]
        public void Authorize_ValidInputs_ReturnFalse(string testTitle, List<Claim> userClaims, Claim functionClaim)
        {
            // Arrange
            Boolean expected = false;
            Boolean actual = true;
            using (_dbContext = new DatabaseContext())
            {
                AuthorizationClient authClient = new AuthorizationClient(_dbContext);

                // Act
                actual = authClient.Authorize(userClaims, functionClaim);
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetNullData), DynamicDataSourceType.Method)]
        public void Authorize_InvalidInputs_ReturnFalse(string testTitle, List<Claim> userClaims, Claim functionClaim)
        {
            // Arrange
            Boolean expected = false;
            Boolean actual = true;
            using (_dbContext = new DatabaseContext())
            {
                AuthorizationClient authClient = new AuthorizationClient(_dbContext);

                // Act
                actual = authClient.Authorize(userClaims, functionClaim);
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

