using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class PasswordServiceTests
    {
        PasswordService ps = new PasswordService();

        [TestMethod]
        public void CheckIfPasswordBreached_InputAsBreachedPassword_Pass()
        {
            var result = ps.CheckIfPasswordBreached("password");
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void CheckIfPasswordBreached_InputAsUnbreachedPassword_Pass()
        {
            var result = ps.CheckIfPasswordBreached("obviouslynotbeenhacked123!");
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Sha1Hash_CovertTwoEqualStringsToSHA1Hash_Pass()
        {
            var string1 = "test123";
            var string2 = "test123";

            Assert.AreEqual(ps.Sha1Hash(string1), ps.Sha1Hash(string2));
        }

        [TestMethod]
        public void Sha1Hash_CovertTwoUnequalStringsToSHA1Hash_Pass()
        {
            var string1 = "test123";
            var string2 = "test";

            Assert.AreNotEqual(ps.Sha1Hash(string1), ps.Sha1Hash(string2));
        }

        [TestMethod]
        public void RfcHashPassword_EqualPasswordsEqualSalt_Pass()
        {
            var pw1 = "testing123";
            var pw2 = "testing123";
            byte[] salt = ps.GetSalt();

            Assert.AreEqual(ps.RfcHashPassword(pw1, salt), ps.RfcHashPassword(pw2, salt));
        }

        [TestMethod]
        public void RfcHashPassword_EqualPasswordsUnequalSalt_Pass()
        {
            var pw1 = "testing123";
            var pw2 = "testing123";
            byte[] salt1 = ps.GetSalt();
            byte[] salt2 = ps.GetSalt();

            Assert.AreNotEqual(ps.RfcHashPassword(pw1, salt1), ps.RfcHashPassword(pw2, salt2));
        }

        [TestMethod]
        public void RfcHashPassword_UnequalPasswordsUnequalSalt_Pass()
        {
            var pw1 = "testtest";
            var pw2 = "testing123";
            byte[] salt1 = ps.GetSalt();
            byte[] salt2 = ps.GetSalt();

            Assert.AreNotEqual(ps.RfcHashPassword(pw1, salt1), ps.RfcHashPassword(pw2, salt2));
        }

        [TestMethod]
        public void RfcHashPassword_UnequalPasswordsEqualSalt_Pass()
        {
            var pw1 = "testtest";
            var pw2 = "testing123";
            byte[] salt = ps.GetSalt();

            Assert.AreNotEqual(ps.RfcHashPassword(pw1, salt), ps.RfcHashPassword(pw2, salt));
        }

        [TestMethod]
        public void GetSalt_CompareTwoSaltUnequal_Pass()
        {
            byte[] salt1 = ps.GetSalt();
            byte[] salt2 = ps.GetSalt();

            Assert.AreNotEqual(salt1, salt2);
        }

    }
}
