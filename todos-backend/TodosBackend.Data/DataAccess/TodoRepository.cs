﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Todo>> GetUserTodos(int userId, int count)
        {
            return await _context.Todos.Where(c => c.UserId == userId).Take(count).ToListAsync();
        }

        public async Task<List<Todo>> GetUserTodosPaged(int userId, int page, int size)
        {
            return await _context.Todos.Where(c => c.UserId == userId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
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
