using Microsoft.EntityFrameworkCore;
using TodosBackend.Data.DataAccess.Abstractions;
using TodosBackend.Data.Models;

namespace TodosBackend.Data.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private TodosDbContext _context;
        public Repository(TodosDbContext context)
        {
            _context = context;
        }
        public T Create(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> GetOneAsync(int id)
        {
            return await _context.Set<T>().Where(c => c.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int page, int size)
        {
            return await _context.Set<T>()
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetSomeAsync(int count)
        {
            return await _context.Set<T>().Take(count).ToListAsync();
        }

        public T Update(T entity)
        {
            return _context.Set<T>().Update(entity).Entity;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
