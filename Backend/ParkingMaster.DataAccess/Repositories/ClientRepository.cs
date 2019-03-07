using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            context = databaseContext;
            dbset = context.Set<Client>();
        }

        public Client GetByEmail(string email)
        {
            return dbset.Find(email);
        }

        public Client GetByName(Client c)
        {
            return dbset.Find(c.Email);
        }

        public List<Claim> GetAllClientClaims(string email)
        {
            Client client = dbset.Find(email);

            if (client != null)
            {
                return client.ClientClaims as List<Claim>;
            }

            return new List<Claim>();
        }

    }
}
