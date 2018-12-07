using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess.Repositories;

namespace ParkingMaster.Services.Tests
{
    /// <summary>
    /// Summary description for UserDeletionUnitTests
    /// </summary>
    [TestClass]
    public class UserDeletionUnitTests
    {
        public UserDeletionUnitTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void UserDeletion_UserExists_Pass()
        {
            // Arrange
            MockRepository mock = new MockRepository();
            UserCreationService creationService = new UserCreationService(mock);
            User user = new User
            {
                Email = "coolguy123@gmail.com",
                Password = "123",
                DateOfBirth = "11/11/2011",
                City = "Long Beach",
                State = "CA",
                Country = "United States"
            };
            creationService.UserCreation(user);
            var expected = true;
            var actual = false;

            // Act
            UserDeletionService deletionService = new UserDeletionService(mock);
            deletionService.UserDeletion(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_NullObjectInput_Fail()
        {
            //Arrange
            UserDeletionService service = new UserDeletionService(new MockRepository());
            User user = null;
            var expected = false;
            var actual = false;
            //Act
            actual = service.UserDeletion(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_UserDNE_Fail()
        {
            //Arrange
            UserDeletionService service = new UserDeletionService(new MockRepository());
            User user = null;
            var expected = false;
            var actual = false;
            //Act
            actual = service.UserDeletion(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
