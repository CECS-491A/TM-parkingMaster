using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class PasswordServiceTests
    {
        PasswordValidationManager pvm = new PasswordValidationManager();

        [TestMethod]
        public void CheckPassword_InputAsBreachedPassword_Fail()
        {
            var hashed = pvm.GetHashedPw("password");
            var result = pvm.CheckPassword(hashed);
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void CheckPassword_InputAsUnbreachedPassword_Pass()
        {
            var hashed = pvm.GetHashedPw("obviouslynothacked!haha1233");
            var result = pvm.CheckPassword(hashed);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void CheckPassword_InputAsEmptyPassword_Pass()
        {
            var hashed = pvm.GetHashedPw("");
            var result = pvm.CheckPassword(hashed);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void GetHashedPw_CovertTwoEqualStringsToSHA1Hash_Pass()
        {
            var string1 = "test123";
            var string2 = "test123";

            Assert.AreEqual(pvm.GetHashedPw(string1), pvm.GetHashedPw(string2));
        }

        [TestMethod]
        public void GetHashedPw_CovertTwoUnequalStringsToSHA1Hash_Pass()
        {
            var string1 = "test123";
            var string2 = "test";

            Assert.AreNotEqual(pvm.GetHashedPw(string1), pvm.GetHashedPw(string2));
        }


    }
}
