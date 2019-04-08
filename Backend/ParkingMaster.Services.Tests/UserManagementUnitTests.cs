using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingMaster.Services.Services;
using ParkingMaster.DataAccess;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;
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
                Username = "plaurent@yahoo.com",
                Id = new Guid("88888888-4444-3333-2222-111111111111"),
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

        [TestMethod]
        public void GetUserClaims_UserExists_Pass()
        {
            string username = "pnguyen@gmail.com";
            List<ClaimDTO> expected = new List<ClaimDTO>
            {
                new ClaimDTO("User", "pnguyen@gmail.com"),
                new ClaimDTO("Action", "DisabledAction"),
                new ClaimDTO("Action", "CreateOtherUser"),
                new ClaimDTO("Action", "Logout"),
                new ClaimDTO("Parent", "client1@yahoo.com")
            };
            List<ClaimDTO> actual = null;
            using (_userGateway = new UserGateway())
            {
                UserManagementService _userManagementService = new UserManagementService(_userGateway);

                // Act
                actual = _userManagementService.GetAllUserClaims(username).Data;
            }
            //Assert
            foreach(ClaimDTO claim in expected)
            {
                CollectionAssert.Contains(actual, claim);
            }
        }

    }
    
}
