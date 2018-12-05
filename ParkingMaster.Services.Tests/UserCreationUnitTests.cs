using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class UserCreationUnitTests
    {
        [TestMethod]
        public void UserCreation_NullObjectInput_Fail()
        {
            //Arrange
            var user = null;
            var expected = false;
            var actual = false;
            //Act
            UserCreationService u = new UserCreationService();
            actual = u.UserCreationService(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserCreation_ValidInput_Pass()
        {
            //Arrange
            var user = new UserDTO
            {
                Email = "ddd",
                Password = "123",
                DateOfBirth = "11/11/2011",
                City = "Long Beach",
                State = "CA",
                Country = "United States"
            };
            var expected = true;
            var actual = false;

            //Act
            actual = actual = u.UserCreationService(user);

            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
