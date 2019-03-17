using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services.Services;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.Models;
using System.Collections.Generic;

namespace ParkingMaster.Services.Tests
{
    [TestClass]
    public class UserManagementUnitTests
    {
        
        private UserGateway _userGateway;

        [TestMethod]
        public void UserCreation_ValidEmail_Pass()
        {
            //Arrange
            var user = new UserAccount()
            {
                Username = "test@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            var claims = new List<Claim>()
            {
                new Claim("User", "test@gmail.com")
            };
            var expected = true;
            var actual = false;
            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                // Act
                actual = _userManagementService.CreateUser(user, claims).Data;
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserCreation_UserAlreadyExists_Fail()
        {
            var user = new UserAccount
            {
                Username = "tnguyen@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            var claims = new List<Claim>()
            {
                new Claim("User", "tnguyen@gmail.com")
            };
            var expected = false;
            var actual = false;
            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                // Act
                actual = _userManagementService.CreateUser(user, claims).Data;
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserCreation_NullInput_Fail()
        {
            UserAccount user = null;
            List<Claim> claims = null;
            var expected = false;
            var actual = false;
            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                // Act
                actual = _userManagementService.CreateUser(user, claims).Data;
            }
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_UserExists_Pass()
        {
            
            var user = new UserAccount
            {
                Username = "tnguyen@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            
            var expected = true;
            var actual = false;

            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                // Act
                actual = _userManagementService.DeleteUser(user).Data;
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_UserDNE_Fail()
        {
            var user = new UserAccount
            {
                Username = "randomemail@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            var expected = false;
            var actual = false;

            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                actual = _userManagementService.DeleteUser(user).Data;
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserDeletion_NullInput_Fail()
        {
            UserAccount user = null;
            var expected = false;
            var actual = false;
            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                // Act
                actual = _userManagementService.DeleteUser(user).Data;
            }
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserConfiguration_UserExists_Pass()
        {
            var user = new UserAccount
            {
                Username = "tnguyen@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            //_userManagementService.CreateUser(user);

            var expected = true;
            var actual = false;

            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                actual = _userManagementService.UpdateUser(user).Data;
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserConfiguration_UserDNE_Fail()
        {
            var user = new UserAccount
            {
                Username = "randomemail@gmail.com",
                IsActive = true,
                IsFirstTimeUser = false,
                RoleType = "standard"
            };
            var expected = false;
            var actual = false;

            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                actual = _userManagementService.UpdateUser(user).Data;
            }
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserConfiguration_NullInput_Fail()
        {
            UserAccount user = null;
            var expected = false;
            var actual = false;
            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                // Act
                actual = _userManagementService.UpdateUser(user).Data;
            }
            //Assert
            Assert.AreEqual(expected, actual);
        }
        

    }
    
}
