using Microsoft.EntityFrameworkCore;
using TodosBackend.Data.DataAccess.Abstractions;
using TodosBackend.Data.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TodosBackend.Data.DataAccess
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        private ApplicationDbContext _context;
        public TodoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetUserTodos(int userId)
        {
            return await _context.Todos.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task ToggleComplete(int id)
        {
            var entity = await GetOneAsync(id);
            if (entity == null)
                throw new ArgumentNullException();

            entity.Completed = !entity.Completed;
            _context.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync();
        }
    }
}
