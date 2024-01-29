using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodosBackend.Models;

namespace TodosBackend.Data.Abstractions
{
    public interface ITodoRepository : IRepository<Todo>
    {
        public Task ToggleComplete(int id);
        public Task<List<Todo>> GetUserTodos(int userId);
    }
}