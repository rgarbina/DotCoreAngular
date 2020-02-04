using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_ASPNETCore.Repositories
{
    public interface IDataRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> SaveAsync(T entity);
        void Dispose();
        IQueryable<T> GetAllAsNoTracking();
        Task<T> GetById(long id);
        Task<bool> Exist(long id);
    }
}
