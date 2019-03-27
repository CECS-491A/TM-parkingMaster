using System;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Services.Services.Contracts
{
    public interface ISessionService
    {
        ResponseDTO<Session> CreateSession(Guid userId);
        ResponseDTO<Session> GetSession(Guid sessionId);
        ResponseDTO<SessionDTO> UpdateSessionExpiration(Guid sessionId);
        ResponseDTO<bool> DeleteSession(Guid sessionId);
    }
}
