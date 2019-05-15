using System.ComponentModel.DataAnnotations;

namespace ParkingMaster.Models.DTO
{
    public class ParkingMasterFrontendDTO
    {
        public string Id { get; set; }
        [Required]
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool AcceptedTOS { get; set; }
    }
}