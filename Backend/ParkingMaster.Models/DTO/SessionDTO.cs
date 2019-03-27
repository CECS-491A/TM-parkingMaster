using System;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Models.DTO
{
    public class SessionDTO
    {
        public Guid UserId { get; set; }
        public DateTime ExpiringAt { get; set; }

        public SessionDTO()
        {
            UserId = Guid.NewGuid();
            ExpiringAt = DateTime.Now.AddMinutes(Session.SESSION_LENGTH);
        }

        // Constructor for easy conversion to DTO
        public SessionDTO(Session session)
        {
            UserId = session.Id;
            ExpiringAt = session.ExpiringAt;
        }

        // Constructor to easily add new session to database
        public SessionDTO(Guid userId)
        {
            UserId = userId;
            ExpiringAt = DateTime.Now.AddMinutes(Session.SESSION_LENGTH);
        }
    }
}
