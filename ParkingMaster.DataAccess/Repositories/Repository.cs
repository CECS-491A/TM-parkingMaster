using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

// Note: Install EntityFramework from the NuGet packages

namespace ParkingMaster.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> dbset;

        public Repository(DbContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }

        public void Insert(T entity)
        {
            context.Entry(entity).State = System.Data.Entity.EntityState.Added; // Something weird is going on here, will fix
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public T GetById(int id)
        {
            return dbset.Find(id);
        }

        // ???
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
