using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
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
            return _sessionGateway.StoreSession(new Session(userId));
        }

        public ResponseDTO<bool> DeleteSession(Guid sessionId)
        {
            return _sessionGateway.DeleteSession(sessionId);
        }

        public ResponseDTO<Session> GetSession(Guid sessionId)
        {
            ResponseDTO<Session> response = _sessionGateway.GetSession(sessionId);

            // If session exists, make sure to update it everytime
            if(response.Data != null)
            {
                response = UpdateSessionExpiration(sessionId);
            }

            return response;
        }

        public ResponseDTO<Session> UpdateSessionExpiration(Guid sessionId)
        {
            return _sessionGateway.UpdateSessionExpiration(sessionId);
        }
    }
}
