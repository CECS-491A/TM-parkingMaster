using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class PasswordServiceTests
    {
        PasswordService ps = new PasswordService();

        [TestMethod]
        public void CheckPassword_InputAsBreachedPassword_Fail()
        {
            var result = ps.CheckPassword("password123");
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void CheckPassword_InputAsUnbreachedPassword_Pass()
        {
            var result = ps.CheckPassword("obviouslynothacked!haha1233");
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void CheckPassword_InputAsEmptyPassword_Pass()
        {
            var result = ps.CheckPassword(" ");
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void CheckPassword_InputAsNull_Pass()
        {

            var result = ps.CheckPassword(null);
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void GetHashedPw_CovertTwoEqualStringsToSHA1Hash_Pass()
        {
            var string1 = "test123";
            var string2 = "test123";

            Assert.AreEqual(ps.Sha1Hash(string1), ps.Sha1Hash(string2));
        }

        [TestMethod]
        public void GetHashedPw_CovertTwoUnequalStringsToSHA1Hash_Pass()
        {
            var string1 = "test123";
            var string2 = "test";

            Assert.AreNotEqual(ps.Sha1Hash(string1), ps.Sha1Hash(string2));
        }

        [TestMethod]
        public void GetHashedPw_InputNullStringReturnsHashOfEmptyString_Pass()
        {
            Assert.AreEqual(ps.Sha1Hash(null), ps.Sha1Hash(""));
        }
    }
}
