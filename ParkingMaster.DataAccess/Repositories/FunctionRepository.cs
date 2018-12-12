using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;
using System.Data.Entity;

namespace ParkingMaster.DataAccess.Repositories
{
    public class FunctionRepository : Repository<Function>, IFunctionRepository
    {
        public FunctionRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            context = databaseContext;
            dbset = context.Set<Function>();
        }

        public Function GetByName(string name)
        {
            return dbset.Find(name);
        }

        public Function GetByName(Function f)
        {
            return dbset.Find(f.Name);
        }

        public Boolean FunctionIsActive(string name)
        {
            Function function = dbset.Find(name);

            if (function != null)
            {
                return function.Active;
            }

            return false;
        }

    }
}
