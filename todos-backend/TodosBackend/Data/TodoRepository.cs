using Microsoft.EntityFrameworkCore;
using TodosBackend.Data.Abstractions;
using TodosBackend.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TodosBackend.Data
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        private TodosDbContext _context;
        public TodoRepository(TodosDbContext context) : base(context)
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

            entity.Completed = !entity.Completed;
            _context.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync();
        }
    }
}
