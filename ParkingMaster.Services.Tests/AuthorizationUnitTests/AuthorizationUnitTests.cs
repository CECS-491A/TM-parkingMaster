using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.Services.Tests.AuthorizationUnitTests
{
    [TestClass]
    public class AuthorizationUnitTests
    {

        [TestMethod]
        public void TestAuthorizeUserToFunction_UserHasAllRequiredClaimsAndNoBannedClaims_ReturnsTrue()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedFunctionClaims = new List<Claim>();
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = true;

            requiredFunctionClaims.Add(new Claim("Role", "SysAdmin"));
            requiredFunctionClaims.Add(new Claim("User", "Jake@email.com"));

            userClaims.Add(new Claim("Role", "SysAdmin"));
            userClaims.Add(new Claim("User", "Jake@email.com"));
            userClaims.Add(new Claim("Random Title", "Random Value"));

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_UserHasAllRequiredClaimsAndHasABannedClaims_ReturnsFalse()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedFunctionClaims = new List<Claim>();
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = false;

            requiredFunctionClaims.Add(new Claim("Role", "SysAdmin"));
            requiredFunctionClaims.Add(new Claim("User", "Jake@email.com"));

            userClaims.Add(new Claim("Role", "SysAdmin"));
            userClaims.Add(new Claim("User", "Jake@email.com"));
            userClaims.Add(new Claim("Random Title", "Random Value"));

            bannedFunctionClaims.Add(new Claim("Random Title", "Random Value"));

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_UserHasAllRequiredClaimsButFunctionIsBlanketBanned_ReturnsFalse()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedFunctionClaims = new List<Claim>();
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = false;

            requiredFunctionClaims.Add(new Claim("Role", "SysAdmin"));
            requiredFunctionClaims.Add(new Claim("User", "Jake@email.com"));

            userClaims.Add(new Claim("Role", "SysAdmin"));
            userClaims.Add(new Claim("User", "Jake@email.com"));
            userClaims.Add(new Claim("Random Title", "Random Value"));

            bannedFunctionClaims.Add(new Claim("Ban All", "Ban All"));

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_UserMissingOneRequiredClaimAndNoBannedClaims_ReturnsFalse()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedFunctionClaims = new List<Claim>();
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = false;

            requiredFunctionClaims.Add(new Claim("Role", "SysAdmin"));
            requiredFunctionClaims.Add(new Claim("User", "Jake@email.com"));

            userClaims.Add(new Claim("Role", "SysAdmin"));
            userClaims.Add(new Claim("Random Title", "Random Value"));

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_NoUserClaimsTwoRequiredClaimsNoBannedClaims_ReturnsFalse()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedFunctionClaims = new List<Claim>();
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = false;

            requiredFunctionClaims.Add(new Claim("Role", "SysAdmin"));
            requiredFunctionClaims.Add(new Claim("User", "Jake@email.com"));

            

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_NoUserClaimsNoRequiredClaimsNoBannedClaims_ReturnsTrue()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedFunctionClaims = new List<Claim>();
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = true;

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_AllInputsNull_ReturnsFalse()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = null;
            List<Claim> userClaims = null;
            List<Claim> bannedFunctionClaims = null;
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = false;
            

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_NoUserClaimsNoRequiredClaimsBannedClaimsIsNull_ReturnsFalse()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedFunctionClaims = null;
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = false;


            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_NoUserClaimsRequiredClaimsIsNullNoBannedClaims_ReturnsFalse()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = null;
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedFunctionClaims = new List<Claim>();
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = false;


            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAuthorizeUserToFunction_UserClaimsIsNullNoRequiredClaimsNoBannedClaims_ReturnsFalse()
        {
            // Arrange
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = null;
            List<Claim> bannedFunctionClaims = new List<Claim>();
            AuthorizationService authorizor = new AuthorizationService();
            Boolean actual;
            Boolean expected = false;


            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
