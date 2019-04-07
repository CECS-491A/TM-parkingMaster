﻿using System.ComponentModel.DataAnnotations;

namespace ParkingMaster.Models.DTO
{
    public class SsoUserRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Signature { get; set; }
        [Required]
        public string SsoUserId { get; set; }
        [Required]
        public long Timestamp { get; set; }

        public string GetStringToSign()
        {
            string acc = "";
            acc += "ssoUserId=" + SsoUserId + ";";
            acc += "email=" + Email + ";";
            acc += "timestamp=" + Timestamp + ";";
            return acc;
        }
    }
}