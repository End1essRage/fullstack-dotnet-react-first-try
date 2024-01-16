using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodosBackend.Data
{
    public class TodoRepository
    {
        private TodosDbContext _context;

        public TodoRepository(TodosDbContext context)
        {
            _context = context;
        }

        public async Task AddTodo(string text)
        {
            var item = new Todo();
            item.Title = text;
            await _context.Todos.AddAsync(item);
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            return await _context.Todos.ToListAsync();
        }
    }
}