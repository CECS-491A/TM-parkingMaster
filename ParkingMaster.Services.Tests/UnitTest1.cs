using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void TestHIBP()
        {
            //arrange
            bool expected = false;
            String input = "thebestpassword";
            System.Collections.Generic.HashSet<String> passwords = new System.Collections.Generic.HashSet<String>();
            passwords.Add("password");
            passwords.Add("pass");
            passwords.Add("hello123");
            //act
            bool actual = passwords.Contains(input);
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
