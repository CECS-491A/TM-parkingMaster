using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using System;
using ParkingMaster.DataAccess.Gateways.Contracts;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Gateways
{
    public class SessionGateway : IDisposable, ISessionGateway
    {
        UserContext context;

        public SessionGateway()
        {
            context = new UserContext();
        }

        public SessionGateway(UserContext c)
        {
            context = c;
        }

        public ResponseDTO<bool> CreateSession(SessionDTO sessionDTO)
        {
            Session session = new Session(sessionDTO);

            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    // Add UserAccount
                    context.Sessions.Add(session);

                    context.SaveChanges();

                    // Commit transaction to database
                    dbContextTransaction.Commit();

                    // Return a true ResponseDto
                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    // Rolls back the changes saved in the transaction
                    dbContextTransaction.Rollback();
                    // Returns a false ResponseDto
                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                    };
                }
            }
        }

        public ResponseDTO<bool> DeleteSession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<SessionDTO> GetSession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<SessionDTO> UpdateSessionExpiration(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose of the context
        /// </summary>
        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
