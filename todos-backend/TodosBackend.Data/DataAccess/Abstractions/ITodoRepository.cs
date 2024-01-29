using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodosBackend.Data.Models;

namespace TodosBackend.Data.DataAccess.Abstractions
{
    public interface ITodoRepository : IRepository<Todo>
    {
        public Task ToggleComplete(int id);
        public Task<List<Todo>> GetUserTodos(int userId, int count);
        public Task<List<Todo>> GetUserTodosPaged(int userId, int page, int size);
    }
}