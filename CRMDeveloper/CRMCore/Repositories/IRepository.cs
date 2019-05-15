using CRMCore.DB.Identity;
using System.Collections.Generic;

namespace CRMCore.Repositories
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        IEnumerable<T> All();
        IEnumerable<T> AllWithDeleted();
        T Get(long key);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(long key);
        void SaveChanges();
    }
}