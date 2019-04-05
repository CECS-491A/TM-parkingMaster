using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using ParkingMaster.Services.Services.Contracts;
using ParkingMaster.DataAccess;

namespace ParkingMaster.Services.Services
{
    public class SessionService : ISessionService
    {
        private SessionGateway _sessionGateway;

        public SessionService()
        {
            _sessionGateway = new SessionGateway();
        }

        public ResponseDTO<Session> CreateSession(Guid userId)
        {
            return _sessionGateway.StoreSession(new SessionDTO(userId));
        }

        public ResponseDTO<bool> DeleteSession(Guid sessionId)
        {
            return _sessionGateway.DeleteSession(sessionId);
        }

        public ResponseDTO<Session> GetSession(Guid sessionId)
        {
            return _sessionGateway.GetSession(sessionId);
        }

        public ResponseDTO<SessionDTO> UpdateSessionExpiration(Guid sessionId)
        {
            return _sessionGateway.UpdateSessionExpiration(sessionId);
        }
    }
}
