using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using System;
using ParkingMaster.DataAccess.Gateways.Contracts;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess
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

        public ResponseDTO<Session> StoreSession(Session session)
        {
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
                    return new ResponseDTO<Session>()
                    {
                        Data = session
                    };
                }
                catch (Exception)
                {
                    // Rolls back the changes saved in the transaction
                    dbContextTransaction.Rollback();
                    // Returns a false ResponseDto
                    return new ResponseDTO<Session>()
                    {
                        Data = null,
                    };
                }
            }
        }

        public ResponseDTO<bool> DeleteSession(Guid sessionId)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {

                    // Queries for the session by sessionId.
                    var session = (from sessions in context.Sessions
                                   where sessions.SessionId == sessionId
                                   select sessions).FirstOrDefault();

                    // Checking if user account is null.
                    if (session == null)
                    {
                        return new ResponseDTO<bool>()
                        {
                            Data = false,
                        };
                    }
                    

                    // Delete useraccount
                    context.Sessions.Remove(session);
                    context.SaveChanges();
                    dbContextTransaction.Commit();

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return new ResponseDTO<bool>()
                    {
                        Data = false,

                    };
                };
            }
        }

        public ResponseDTO<Session> GetSession(Guid sessionId)
        {
            try
            {
                var session = (from sessions in context.Sessions
                                   where sessions.SessionId == sessionId
                                   select sessions).FirstOrDefault();

                // Return a ResponseDto with a Session DTO
                return new ResponseDTO<Session>()
                {
                    Data = (Session) session
                };
            }
            catch (Exception)
            {
                return new ResponseDTO<Session>()
                {
                    Data = null
                };
            }
        }

        public ResponseDTO<Session> UpdateSessionExpiration(Guid sessionId)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Queries for the session that needs to be updated.
                    var session = (from sessions in context.Sessions
                                   where sessions.SessionId == sessionId
                                   select sessions).First();
                    
                    session.ExpiringAt = DateTime.Now.AddMinutes(Session.SESSION_LENGTH);
                    context.SaveChanges();
                    dbContextTransaction.Commit();

                    return new ResponseDTO<Session>()
                    {
                        Data = session
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<Session>()
                    {
                        Data = null,
                        Error = "Unable to update sessionId: " + sessionId
                    };
                }
            }
        }

        // Returns all sessions in the data store
        public ResponseDTO<List<Session>> GetAllSessions()
        {
            ResponseDTO<List<Session>> response = new ResponseDTO<List<Session>>();
            try
            {
                response.Data = context.Sessions.ToList<Session>();
                return response;
            }
            catch
            {
                response.Data = null;
                response.Error = "Failed to read Session table";
                return response;
            }
        }

        // Deletes all sessions to reinitialize the database
        public void ResetDatabase()
        {

            List<Session> sessions = GetAllSessions().Data;

            if (sessions == null)
            {
                return;
            }
            foreach (Session session in sessions)
            {
                DeleteSession(session.SessionId);
            }

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
