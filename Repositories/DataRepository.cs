using Angular_ASPNETCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_ASPNETCore.Repositories
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public DataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> SaveAsync(T entity)
        {
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> GetAllAsNoTracking()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetAllTracking()
        {
            return _context.Set<T>();
        }
        public async Task<bool> Exist(long id)
        {
            var t = await GetById(id);
            return t == null;
        }
        public async Task<T> GetById(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
