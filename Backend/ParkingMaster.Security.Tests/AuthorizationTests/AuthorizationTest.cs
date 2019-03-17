using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.DataAccess;
using ParkingMaster.Security.Authorization;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Security.Tests.AuthorizationTests
{
    [TestClass]
    public class AuthorizationTest
    {

        private static List<ClaimDTO> logoutClaims = new List<ClaimDTO>(){
            new ClaimDTO("Action", "Logout")
        };
        private static List<ClaimDTO> createOtherUserClaims = new List<ClaimDTO>(){
            new ClaimDTO("Action", "CreateOtherUser")
        };
        private static List<ClaimDTO> disabledActionClaims = new List<ClaimDTO>(){
            new ClaimDTO("Action", "DisabledAction")
        };
        private static List<ClaimDTO> client2ActionClaims = new List<ClaimDTO>(){
            new ClaimDTO("Action", "Client2Action")
        };

private static IEnumerable<object[]> GetPassData()
        {

            yield return new object[] { "pnguyen@gmail.com attempts to logout.  ", "pnguyen@gmail.com", logoutClaims };
            yield return new object[] { "tnguyen@gmail.com attempts to logout.  ", "tnguyen@gmail.com", logoutClaims };
            yield return new object[] { "client1@yahoo.com attempts to logout.  ", "client1@yahoo.com", logoutClaims };
            yield return new object[] { "client2@gmail.com attempts to logout.  ", "client2@gmail.com", logoutClaims };

            yield return new object[] { "pnguyen@gmail.com attempts to create other user.  ", "pnguyen@gmail.com", createOtherUserClaims };
            yield return new object[] { "client1@yahoo.com attempts to create other user.  ", "client1@yahoo.com", createOtherUserClaims };
            yield return new object[] { "client2@gmail.com attempts to create other user.  ", "client2@gmail.com", createOtherUserClaims };

            yield return new object[] { "tnguyen@gmail.com attempts client 2 action.  ", "tnguyen@gmail.com", client2ActionClaims };
            yield return new object[] { "client2@gmail.com attempts client 2 action.  ", "client2@gmail.com", client2ActionClaims };

        }

        private static IEnumerable<object[]> GetFailData()
        {

            yield return new object[] { "tnguyen@gmail.com attempts to create other user.  ", "tnguyen@gmail.com", createOtherUserClaims };

            yield return new object[] { "pnguyen@gmail.com attempts client 2 action.  ", "pnguyen@gmail.com", client2ActionClaims };
            yield return new object[] { "client1@yahoo.com attempts client 2 action.  ", "client1@yahoo.com", client2ActionClaims };

            yield return new object[] { "pnguyen@gmail.com attempts disabled action.  ", "pnguyen@gmail.com", disabledActionClaims };
            yield return new object[] { "tnguyen@gmail.com attempts disabled action.  ", "tnguyen@gmail.com", disabledActionClaims };
            yield return new object[] { "client1@yahoo.com attempts disabled action.  ", "client1@yahoo.com", disabledActionClaims };
            yield return new object[] { "client2@gmail.com attempts disabled action.  ", "client2@gmail.com", disabledActionClaims };
        }

        private static IEnumerable<object[]> GetNullData()
        {

            yield return new object[] { "All null inputs.  ", null, null };
            yield return new object[] { "User claims null with valid function.  ", null, client2ActionClaims };
            yield return new object[] { "Valid user claims with null function.  ", "client1@yahoo.com", null };
        }



        [DataTestMethod]
        [DynamicData(nameof(GetPassData), DynamicDataSourceType.Method)]
        public void Authorize_ValidInputs_ReturnsTrue(string testTitle, string username, List<ClaimDTO> functionClaims)
        {
            // Arrange
            Boolean expected = true;
            Boolean actual = false;
            UserContext userContext = new UserContext();

            AuthorizationClient authClient = new AuthorizationClient();

            // Act
            actual = authClient.Authorize(username, functionClaims).Data;
            

            

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [DataTestMethod]
        [DynamicData(nameof(GetFailData), DynamicDataSourceType.Method)]
        public void Authorize_ValidInputs_ReturnFalse(string testTitle, string username, List<ClaimDTO> functionClaims)
        {
            // Arrange
            Boolean expected = false;
            Boolean actual = true;
            
            AuthorizationClient authClient = new AuthorizationClient();

            // Act
            actual = authClient.Authorize(username, functionClaims).Data;
            

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetNullData), DynamicDataSourceType.Method)]
        public void Authorize_InvalidInputs_ReturnFalse(string testTitle, string username, List<ClaimDTO> functionClaims)
        {
            // Arrange
            Boolean expected = false;
            Boolean actual = true;
            AuthorizationClient authClient = new AuthorizationClient();

            // Act
            actual = authClient.Authorize(username, functionClaims).Data;
            

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
    }
}

