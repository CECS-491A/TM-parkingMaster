using System;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;


namespace ParkingMaster.DataAccess.Gateways.Contracts
{
    public interface ISessionGateway
    {
        ResponseDTO<Session> StoreSession(Session sessionDTO);
        ResponseDTO<Session> GetSession(Guid sessionId);
        ResponseDTO<Session> UpdateSessionExpiration(Guid sessionId);
        ResponseDTO<bool> DeleteSession(Guid sessionId);

    }
}
