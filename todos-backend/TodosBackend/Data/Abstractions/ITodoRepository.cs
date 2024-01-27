using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodosBackend.Models;

namespace TodosBackend.Data.Abstractions
{
    public interface ITodoRepository
    {
        public Task<Todo> GetTodoById(int id);
        public Task<Todo> AddTodo(Todo todo);
        public Task<List<Todo>> GetAllTodos();
        public Task ToggleComplete(int id);
        public Task DeleteTodo(int id);
        public Task<List<Todo>> GetUserTodos(int userId);
    }
}