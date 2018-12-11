using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class UserManagementUnitTests
    {
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
