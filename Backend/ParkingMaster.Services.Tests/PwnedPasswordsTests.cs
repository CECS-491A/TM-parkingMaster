using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class PwnedPasswordsTests
    {
        //arrange
        PwnedPasswords pp = new PwnedPasswords();

        [TestMethod]
        public void GetHashListByPrefix_ValidPrefix_Pass()
        {
            //Act
            string validPrefix = "AB9EF";
            var result = pp.GetHashListByPrefix(validPrefix);

            Assert.AreNotEqual(result.Length, 0);
        }

        [TestMethod]
        public void GetHashListByPrefix_InvalidPrefix_Pass()
        {
            string invalidPrefix = "ZZ9FA";
            var result = pp.GetHashListByPrefix(invalidPrefix);

            Assert.AreEqual(result.Length, 0);
        }
    }
}
