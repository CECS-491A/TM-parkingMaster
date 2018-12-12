using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.Security.Authorization;

namespace ParkingMaster.Security.Tests.AuthorizationTests
{
    [TestClass]
    public class MockAuthorizationTest
    {
        private static MockClaimRepository GetMockClaimRepository()
        {
            MockClaimRepository mockRepo = new MockClaimRepository();

            mockRepo.Insert(new EntityClaim("user1@email.com", new Claim("User", "user1@email.com")));
            mockRepo.Insert(new EntityClaim("user1@email.com", new Claim("Action", "DisabledAction")));
            mockRepo.Insert(new EntityClaim("user1@email.com", new Claim("Action", "CreateOtherUser")));
            mockRepo.Insert(new EntityClaim("user1@email.com", new Claim("Action", "Logout")));
            mockRepo.Insert(new EntityClaim("user1@email.com", new Claim("Action", "Client2Action")));
            mockRepo.Insert(new EntityClaim("user1@email.com", new Claim("Client", "client1@email.com")));

            mockRepo.Insert(new EntityClaim("client1@email.com", new Claim("User", "client1@email.com")));
            mockRepo.Insert(new EntityClaim("client1@email.com", new Claim("Action", "DisabledAction")));
            mockRepo.Insert(new EntityClaim("client1@email.com", new Claim("Action", "CreateOtherUser")));
            mockRepo.Insert(new EntityClaim("client1@email.com", new Claim("Action", "Logout")));

            mockRepo.Insert(new EntityClaim("user2@email.com", new Claim("User", "user2@email.com")));
            mockRepo.Insert(new EntityClaim("user2@email.com", new Claim("Action", "Logout")));
            mockRepo.Insert(new EntityClaim("user2@email.com", new Claim("Action", "Client2Action")));
            mockRepo.Insert(new EntityClaim("user2@email.com", new Claim("Client", "client2@email.com")));

            mockRepo.Insert(new EntityClaim("client2@email.com", new Claim("User", "client2@email.com")));
            mockRepo.Insert(new EntityClaim("client2@email.com", new Claim("Action", "CreateOtherUser")));
            mockRepo.Insert(new EntityClaim("client2@email.com", new Claim("Action", "Logout")));
            mockRepo.Insert(new EntityClaim("client2@email.com", new Claim("Action", "Client2Action")));

            mockRepo.AddFunction(new Function("DisabledAction", false));
            mockRepo.AddFunction(new Function("CreateOtherUser", true));
            mockRepo.AddFunction(new Function("Logout", true));
            mockRepo.AddFunction(new Function("Client2Action", true));

            return mockRepo;
        }

        private static List<Claim> GetUserOneClaims()
        {
            List<Claim> userClaims = new List<Claim>();

            userClaims.Add(new Claim("Action", "Logout"));
            userClaims.Add(new Claim("User", "user1@email.com"));
            userClaims.Add(new Claim("Client", "client1@email.com"));

            return userClaims;
        }

        private static List<Claim> GetUserTwoClaims()
        {
            List<Claim> userClaims = new List<Claim>();

            userClaims.Add(new Claim("Action", "Logout"));
            userClaims.Add(new Claim("User", "user2@email.com"));
            userClaims.Add(new Claim("Client", "client2@email.com"));

            return userClaims;
        }

        private static List<Claim> GetClientOneClaims()
        {
            List<Claim> clientClaims = new List<Claim>();

            clientClaims.Add(new Claim("Action", "Logout"));
            clientClaims.Add(new Claim("User", "client1@email.com"));

            return clientClaims;
        }

        private static List<Claim> GetClientTwoClaims()
        {
            List<Claim> clientClaims = new List<Claim>();

            clientClaims.Add(new Claim("Action", "Logout"));
            clientClaims.Add(new Claim("User", "client2@email.com"));

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
            MockClaimRepository _repository = GetMockClaimRepository();
            MockAuthorizationClient authClient = new MockAuthorizationClient(_repository);

            // Act
            actual = authClient.Authorize(userClaims, functionClaim);

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
            MockClaimRepository _repository = GetMockClaimRepository();
            MockAuthorizationClient authClient = new MockAuthorizationClient(_repository);

            // Act
            actual = authClient.Authorize(userClaims, functionClaim);

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
            MockClaimRepository _repository = GetMockClaimRepository();
            MockAuthorizationClient authClient = new MockAuthorizationClient(_repository);

            // Act
            actual = authClient.Authorize(userClaims, functionClaim);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
