using Microsoft.EntityFrameworkCore;
using TodosBackend.Data.Abstractions;
using TodosBackend.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TodosBackend.Data
{
    public class TodoRepository : ITodoRepository
    {
        private TodosDbContext _context;
        public TodoRepository(TodosDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> AddTodo(string title)
        {
            var newTodo = new Todo { Title = title };
            var result = _context.Todos.Add(newTodo);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteTodo(int id)
        {
            var entity = await GetTodoById(id);
            _context.Todos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo> GetTodoById(int id)
        {
            return await _context.Todos.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task ToggleComplete(int id)
        {
            var entity = await GetTodoById(id);
            entity.Completed = !entity.Completed;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
