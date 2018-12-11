using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace ParkingMaster.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> dbset;

        //public Repository()
        //{
        //}

        public Repository(DbContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }

        public void Insert(T entity)
        {
            context.Entry(entity).State = System.Data.Entity.EntityState.Added;
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

        //public T GetByEmail(string email)
        //{
        //    return dbset.Find(email);
        //}

        public IEnumerable<T> GetAll()
        {
            return dbset;
        }

        // ???
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
