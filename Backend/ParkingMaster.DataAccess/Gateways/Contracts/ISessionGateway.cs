using System;
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;


namespace ParkingMaster.DataAccess.Gateways.Contracts
{
    public interface ISessionGateway
    {
        ResponseDTO<bool> CreateSession(SessionDTO sessionDTO);
        ResponseDTO<SessionDTO> GetSession(Guid sessionId);
        ResponseDTO<SessionDTO> UpdateSessionExpiration(Guid sessionId);
        ResponseDTO<bool> DeleteSession(Guid sessionId);

    }
}
