using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess.Repositories;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class UserCreationUnitTests
    {
        [TestMethod]
        public void UserCreation_ValidInput_Pass()
        {
            //Arrange
            UserCreationService service = new UserCreationService(new MockRepository());
            User user = new User
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
            actual = service.UserCreation(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserCreation_NullObjectInput_Fail()
        {
            //Arrange
            UserCreationService service = new UserCreationService(new MockRepository());
            User user = null;
            var expected = false;
            var actual = false;
            //Act
            actual = service.UserCreation(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
