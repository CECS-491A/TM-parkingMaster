using System;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;


namespace ParkingMaster.DataAccess.Gateways.Contracts
{
    public interface ISessionGateway
    {
        ResponseDTO<Session> StoreSession(SessionDTO sessionDTO);
        ResponseDTO<Session> GetSession(Guid sessionId);
        ResponseDTO<SessionDTO> UpdateSessionExpiration(Guid sessionId);
        ResponseDTO<bool> DeleteSession(Guid sessionId);

    }
}
