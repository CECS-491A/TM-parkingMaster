﻿using System;
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
        [TestMethod]
        public void UserCreation_ValidEmail_Pass()
        {
            //Arrange
            UserManagementService _userManagementService = new UserManagementService(new DatabaseContext());
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
            //Act
            //actual = _userManagementService.CreateUser(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserCreation_UserAlreadyExists_Fail()
        {
            UserManagementService _userManagementService = new UserManagementService(new DatabaseContext());
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

            //Act
            //actual = _userManagementService.CreateUser(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_UserExists_Pass()
        {
            UserManagementService _userManagementService = new UserManagementService(new DatabaseContext());
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

            //Act
            //actual = _userManagementService.DeleteUser(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_UserDNE_Fail()
        {
            UserManagementService _userManagementService = new UserManagementService(new DatabaseContext());
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

            //Act
            //actual = _userManagementService.DeleteUser(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserConfiguration_UserExists_Pass()
        {
            UserManagementService _userManagementService = new UserManagementService(new DatabaseContext());
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

            //Act
            //actual = _userManagementService.UpdateUser(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserConfiguration_UserDNE_Fail()
        {
            UserManagementService _userManagementService = new UserManagementService(new DatabaseContext());
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

            //Act
            //actual = _userManagementService.UpdateUser(user);

            //Assert
            Assert.AreEqual(expected, actual);
        }


    }
    
}
