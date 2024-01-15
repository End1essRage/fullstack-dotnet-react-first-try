using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodosBackend.Data
{
    public interface ITodoRepository
    {
        public Task AddTodo(string text);
        public Task<List<Todo>> GetAllTodos();
    }
}