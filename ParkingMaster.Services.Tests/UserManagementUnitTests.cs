using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services.Services;
using ParkingMaster.DataAccess.Models;
using ParkingMaster.DataAccess.Repositories;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class UserManagementUnitTests
    {
        private DatabaseContext _dbContext;

        [TestMethod]
        public void UserCreation_ValidEmail_Pass()
        {
            //Arrange
            var user = new User()
            {
                Email = "test@gmail.com",
                Password = "password",
                DateOfBirth = "01/01/1970",
                City = "City",
                State = "State",
                Country = "Country",
                Role = "Role",
                Activated = false
            };
            var expected = true;
            var actual = false;
            using (_dbContext = new DatabaseContext())
            {
                UserManagementService _userManagementService = new UserManagementService(_dbContext);

                // Act
                actual = _userManagementService.CreateUser(user);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserCreation_UserAlreadyExists_Fail()
        {
            var user = new User
            {
                Email = "tnguyen@gmail.com",
                Password = "123456",
                DateOfBirth = "12/25/1950",
                City = "Albany",
                State = "NY",
                Country = "US",
                Role = "Standard",
                Activated = false
            };
            var expected = false;
            var actual = false;
            using (_dbContext = new DatabaseContext())
            {
                UserManagementService _userManagementService = new UserManagementService(_dbContext);

                // Act
                actual = _userManagementService.CreateUser(user);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_UserExists_Pass()
        {
            
            var user = new User
            {
                Email = "tnguyen@gmail.com",
                Password = "123456",
                DateOfBirth = "12/25/1950",
                City = "Albany",
                State = "NY",
                Country = "US",
                Role = "Standard",
                Activated = false
            };
            
            var expected = true;
            var actual = false;

            using (_dbContext = new DatabaseContext())
            {
                UserManagementService _userManagementService = new UserManagementService(_dbContext);

                // Act
                actual = _userManagementService.DeleteUser(user);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_UserDNE_Fail()
        {
            var user = new User
            {
                Email = "randomemail@gmail.com",
                Password = "123456",
                DateOfBirth = "12/25/1950",
                City = "Albany",
                State = "NY",
                Country = "US",
                Role = "Standard",
                Activated = false
            };
            var expected = false;
            var actual = false;

            using (_dbContext = new DatabaseContext())
            {
                UserManagementService _userManagementService = new UserManagementService(_dbContext);

                actual = _userManagementService.DeleteUser(user);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserConfiguration_UserExists_Pass()
        {
            var user = new User
            {
                Email = "tnguyen@gmail.com",
                Password = "123456",
                DateOfBirth = "12/25/1950",
                City = "Albany",
                State = "NY",
                Country = "US",
                Role = "Standard",
                Activated = false
            };
            //_userManagementService.CreateUser(user);

            var expected = true;
            var actual = false;

            using (_dbContext = new DatabaseContext())
            {
                UserManagementService _userManagementService = new UserManagementService(_dbContext);

                actual = _userManagementService.UpdateUser(user);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserConfiguration_UserDNE_Fail()
        {
            var user = new User
            {
                Email = "randomemail@gmail.com",
                Password = "123456",
                DateOfBirth = "12/25/1950",
                City = "Albany",
                State = "NY",
                Country = "US",
                Role = "Standard",
                Activated = false
            };
            var expected = false;
            var actual = false;

            using (_dbContext = new DatabaseContext())
            {
                UserManagementService _userManagementService = new UserManagementService(_dbContext);

                actual = _userManagementService.UpdateUser(user);
            }
            //Assert
            Assert.AreEqual(expected, actual);
        }


    }
    
}
