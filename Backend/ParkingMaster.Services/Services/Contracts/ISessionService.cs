using System;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;

namespace ParkingMaster.Services.Services
{
    public interface ISessionService
    {
        ResponseDTO<Session> CreateSession(Guid userId);
        ResponseDTO<Session> GetSession(Guid sessionId);
        ResponseDTO<Session> UpdateSessionExpiration(Guid sessionId);
        ResponseDTO<bool> DeleteSession(Guid sessionId);
    }
}
