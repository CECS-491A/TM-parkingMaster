using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Models.Models
{
    [Table("ParkingMaster.Sessions")]
    public class Session
    {
        public static int SESSION_LENGTH = 20;

        [Key]
        public Guid SessionId { get; set; }
        [Required, ForeignKey("UserAccount")]
        [Index(IsUnique = false)]
        public Guid Id { get; set; }
        [Required]
        public DateTime ExpiringAt { get; set; }

        // Navigation Property
        public virtual UserAccount UserAccount { get; set; }

        public Session()
        {
            SessionId = Guid.NewGuid();
            ExpiringAt = DateTime.Now.AddMinutes(SESSION_LENGTH);
        }

        public Session(SessionDTO sessionDTO)
        {
            SessionId = Guid.NewGuid();
            Id = sessionDTO.UserId;
            ExpiringAt = sessionDTO.ExpiringAt;
        }
    }
}
