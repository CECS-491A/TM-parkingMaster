using System.ComponentModel.DataAnnotations;

namespace ParkingMaster.Models.DTO
{
    public class ParkingMasterFrontendDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public long Timestamp { get; set; }
    }
}
