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
            // Attempt to find session with corresponding Id
            ResponseDTO<Session> sessionDTO = _sessionGateway.GetSession(sessionId);

            // If session data is null then an error occured
            if (sessionDTO.Data == null)
            {
                return sessionDTO;
            }

            // Check if session is expired
            if(DateTime.Now.CompareTo(sessionDTO.Data.ExpiringAt) == 1)
            {
                // Because session is no longer valid, remove it from the data store
                _sessionGateway.DeleteSession(sessionId);

                return new ResponseDTO<Session>()
                {
                    Data = null,
                    Error = "Session has expired"
                };
            }

            // Update the session expiration and return session
            return _sessionGateway.UpdateSessionExpiration(sessionId);

        }

        public ResponseDTO<bool> DeleteAllUserSessions(Guid userId)
        {
            return _sessionGateway.DeleteAllUserSessions(userId);
        }

        public ResponseDTO<Session> UpdateSessionExpiration(Guid sessionId)
        {
            return _sessionGateway.UpdateSessionExpiration(sessionId);
        }
    }
}
