using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMaster.DataAccess.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        void Insert(T entity);

        void Delete(T entity);

        void Update(T entity);

        T GetById(int id);

    }
}
