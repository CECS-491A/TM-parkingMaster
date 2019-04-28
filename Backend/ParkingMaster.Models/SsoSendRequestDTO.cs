using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkingMaster.Models.DTO
{
    public class SsoSendRequestDTO
    {
        [Required]
        public string AppId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Signature { get; set; }
        [Required]
        public string SsoUserId { get; set; }
        [Required]
        public long Timestamp { get; set; }

        public Dictionary<string, string> GetStringToSign()
        {
            var launchPayload = new Dictionary<string, string>();
            launchPayload.Add("ssoUserId", SsoUserId);
            launchPayload.Add("email", Email);
            launchPayload.Add("timestamp", Timestamp.ToString());
            return launchPayload;
        }
    }
}