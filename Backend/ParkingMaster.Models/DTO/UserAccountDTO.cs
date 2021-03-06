﻿using ParkingMaster.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.Models.DTO
{
    public class UserAccountDTO
    {
        // Automatic Properties
        [Required]
        public string Username { get; set; }
        public Guid Id { get; set; }
        public Guid SsoId { get; set; }
        public bool IsActive { get; set; }
        public bool AcceptedTOS { get; set; }
        public string RoleType { get; set; }

        // Constructors
        public UserAccountDTO()
        {
            Username = "";
            RoleType = "";
        }

        // Contructor to easily transfer UserAccount into a DTO
        // No need to pass SsoId around
        public UserAccountDTO(UserAccount userAccount)
        {
            Id = userAccount.Id;
            Username = userAccount.Username;
            SsoId = userAccount.SsoId;
            IsActive = userAccount.IsActive;
            AcceptedTOS = userAccount.AcceptedTOS;
            RoleType = userAccount.RoleType;
        }
    }
}