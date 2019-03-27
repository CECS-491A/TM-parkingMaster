using System;
using ParkingMaster.Models.DTO;

namespace ParkingMaster.Services.Services.Contracts
{
    public interface ISessionService
    {
        ResponseDTO<SessionDTO> CreateSession(Guid userId);
        ResponseDTO<SessionDTO> GetSession(Guid sessionId);
        ResponseDTO<SessionDTO> UpdateSessionExpiration(Guid sessionId);
        ResponseDTO<bool> DeleteSession(Guid sessionId);
    }
}
