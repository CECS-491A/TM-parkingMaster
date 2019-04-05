using System.ComponentModel.DataAnnotations;

namespace ParkingMaster.Models.DTO
{
    public class SsoUserRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Signature { get; set; }
        [Required]
        public string SsoId { get; set; }
        [Required]
        public long Timestamp { get; set; }

        public string GetStringToSign()
        {
            return "ssoUserId=" + SsoId + ";email=" + Email + ";timestamp=" + Timestamp + ";";
        }
    }
}
