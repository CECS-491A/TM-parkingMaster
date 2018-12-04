using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.Services.Tests.AuthorizationUnitTests
{
    [TestClass]
    public class AuthorizationUnitTests
    {
        public static IEnumerable<object[]> GetPassData()
        {
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedClaims = new List<Claim>();
            yield return new object[] { "Test 1: No claims in any list    ", requiredFunctionClaims, userClaims, bannedClaims };

            userClaims.Add(new Claim("Role", "Admin"));
            userClaims.Add(new Claim("Username", "user@email.com"));
            userClaims.Add(new Claim("Random Title", "Random Value"));

            yield return new object[] { "Test 2: No required or banned claims, user has 3 claims    ", requiredFunctionClaims, userClaims, bannedClaims };

            requiredFunctionClaims.Add(new Claim("Role", "Admin"));
            requiredFunctionClaims.Add(new Claim("Username", "user@email.com"));
            yield return new object[] { "Test 3: Two required claims and user has both, no banned claims    ", requiredFunctionClaims, userClaims, bannedClaims };

            bannedClaims.Add(new Claim("Username", "bannedUser@email.com"));
            yield return new object[] { "Test 4: Two required claims and user has both, one banned claim that user does not have    ", requiredFunctionClaims, userClaims, bannedClaims };

        }

        public static IEnumerable<object[]> GetFailData()
        {
            List<Claim> requiredFunctionClaims = new List<Claim>();
            List<Claim> userClaims = new List<Claim>();
            List<Claim> bannedClaims = new List<Claim>();

            requiredFunctionClaims.Add(new Claim("Role", "SysAdmin"));
            requiredFunctionClaims.Add(new Claim("Random Title", "Random Value"));

            yield return new object[] { "Test 1: No user claims  two require claims  no banned claims  ", requiredFunctionClaims, userClaims, bannedClaims };

            userClaims.Add(new Claim("Role", "SysAdmin"));
            userClaims.Add(new Claim("Username", "user@email.com"));

            yield return new object[] { "Test 2: Two required claims  three user claims  no banned claims  user missing a required claim  ", requiredFunctionClaims, userClaims, bannedClaims };

            userClaims.Add(new Claim("Random Title", "Random Value"));

            bannedClaims.Add(new Claim("Username", "user@email.com"));
            
            yield return new object[] { "Test 3: Two required claims and user has both, one user claim is in the banned list  ", requiredFunctionClaims, userClaims, bannedClaims };

            bannedClaims.Remove(new Claim("Username", "user@email.com"));
            bannedClaims.Add(new Claim("Ban All", "Ban All"));
            yield return new object[] { "Test 4: User has all required claims  banned claims include the blanket ban  ", requiredFunctionClaims, userClaims, bannedClaims };

        }

        public static IEnumerable<object[]> GetNullData()
        {
            List<Claim> nullRequiredFunctionClaims = null;
            List<Claim> nullUserClaims = null;
            List<Claim> nullBannedClaims = null;

            List<Claim> emptyRequiredFunctionClaims = new List<Claim>();
            List<Claim> emptyUserClaims = new List<Claim>();
            List<Claim> emptyBannedClaims = new List<Claim>();

            List<Claim> containsNullRequiredFunctionClaims = new List<Claim> { null };
            List<Claim> containsNullUserClaims = new List<Claim> { null };
            List<Claim> containsNullBannedClaims = new List<Claim> { null };

            List<Claim>[] functionClaims = new List<Claim>[] { nullRequiredFunctionClaims, emptyRequiredFunctionClaims, containsNullRequiredFunctionClaims };
            List<Claim>[] userClaims = new List<Claim>[] { nullUserClaims, emptyUserClaims, containsNullUserClaims };
            List<Claim>[] bannedClaims = new List<Claim>[] { nullBannedClaims, emptyBannedClaims, containsNullBannedClaims };

            object[] testCase = new object[4];
            string[] testCaseTitle = new string[3];
            int count = 1;

            for(int f = 0; f<3; f++)
            {
                testCaseTitle[0] = "  Required Claims: " + switchType(f);
                testCase[1] = functionClaims[f];
                
                for(int u = 0; u<3; u++)
                {
                    testCaseTitle[1] = "  User Claims: " + switchType(u); 
                    testCase[2] = userClaims[u];

                    for(int b = 0; b<3; b++)
                    {
                        testCaseTitle[2] = "  Banned Claims: " + switchType(b);
                        testCase[3] = userClaims[b];
                        testCase[0] = "Test Case: " + count + testCaseTitle[0] + testCaseTitle[1] + testCaseTitle[2];

                        // 14th test case would be all empty sets which should pass
                        if(count != 14)
                        {
                            yield return testCase;
                        }

                        count++;
                    }
                }
            }
        }

        private static string switchType(int number)
        {
            switch (number)
            {
                case 0:
                    return " null  ";
                case 1:
                    return " empty  ";
                case 2:
                    return " contains empty claim  ";
            }

            return "----ERROR----";
        }

        [DataTestMethod]
        [DynamicData(nameof(GetPassData), DynamicDataSourceType.Method)]
        public void TestAuthorizeUserToFunction_UserHasAccessToFunction_ReturnsTrue(String testTitle, List<Claim> requiredFunctionClaims, List<Claim> userClaims, List<Claim> bannedFunctionClaims)
        {
            // Arrange
            Boolean actual;
            Boolean expected = true;
            AuthorizationService authorizor = new AuthorizationService();

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetFailData), DynamicDataSourceType.Method)]
        public void TestAuthorizeUserToFunction_UserDoesNotHaveAccessToFunction_ReturnsFalse(String testTitle, List<Claim> requiredFunctionClaims, List<Claim> userClaims, List<Claim> bannedFunctionClaims)
        {
            // Arrange
            Boolean actual;
            Boolean expected = false;
            AuthorizationService authorizor = new AuthorizationService();

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetNullData), DynamicDataSourceType.Method)]
        public void TestAuthorizeUserToFunction_PossibleNullInputs_ReturnsFalse(String testTitle, List<Claim> requiredFunctionClaims, List<Claim> userClaims, List<Claim> bannedFunctionClaims)
        {
            // Arrange
            Boolean actual;
            Boolean expected = false;
            AuthorizationService authorizor = new AuthorizationService();

            // Act
            actual = authorizor.AuthorizeUserToFunction(requiredFunctionClaims, userClaims, bannedFunctionClaims);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
    }
}
