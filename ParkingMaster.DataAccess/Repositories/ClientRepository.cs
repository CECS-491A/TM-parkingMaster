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

        public Client GetByName(string email)
        {
            return dbset.Find(email);
        }

        public Client GetByName(Client c)
        {
            return dbset.Find(c.Email);
        }

    }
}
