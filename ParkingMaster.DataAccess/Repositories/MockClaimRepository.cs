using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingMaster.DataAccess.Models;

namespace ParkingMaster.DataAccess.Repositories
{
    public class MockClaimRepository : IClaimRepository
    {
        protected List<EntityClaim> Claims { get; set; }
        protected List<Function> Functions { get; set; }

        public MockClaimRepository()
        {
            Claims = new List<EntityClaim>();
            Functions = new List<Function>();
        }

        public void Insert(EntityClaim e)
        {
            Claims.Add(e);
        }

        public void AddClaim(string owner, Claim claim)
        {
            EntityClaim c = new EntityClaim(owner, claim);
            Claims.Add(c);
        }

        public void AddFunction(Function f)
        {
            Functions.Add(f);
        }
        
        public void AddFunction(string name, Boolean active)
        {
            Function f = new Function(name, active);
            Functions.Add(f);
        }

        public Boolean FunctionIsActive(string function)
        {
            Function f = Functions.Find(x => x.Name == function);
            if (f != null)
            {
                return f.Active;
            }
            return false;
        }

        public List<EntityClaim> GetUserClaims(string owner)
        {
            List<EntityClaim> list = new List<EntityClaim>();
            foreach(EntityClaim e in Claims)
            {
                if (e.Owner == owner) { list.Add(e); }
            }
            return list;
        }

        public void Delete(EntityClaim e)
        {
            Claims.Remove(e);
        }

        public void Update(EntityClaim e)
        {
            //make updates to a claim
        }

        //ignore this for now
        public EntityClaim GetByEmail(string email)
        {
            return Claims.Find(x => x.Owner == email);
        }

        public IEnumerable<EntityClaim> GetAll()
        {
            return Claims;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
