namespace TodosBackend.Data
{
    public class FakeTodosRepository : ITodoRepository
    {
        private FakeDbContext _context;
        public FakeTodosRepository(FakeDbContext context) {
            _context = context;
        }

        public async Task<Todo> GetTodoById(int id)
        {
            return await Task.Run(() => _context.Todos.FirstOrDefault(c => c.Id == id));
        }

        public async Task<Todo> AddTodo(string text)
        {
            int newId = _context.Todos.Count > 0 ? _context.Todos.Max(x => x.Id) + 1 : 0;
            var newTodo = new Todo { Id = newId, Title = text };

            _context.Todos.Add(newTodo);

            return await Task.Run(() => newTodo);
        }

        public Task DeleteTodo(int id)
        {
            var item = _context.Todos.First(c => c.Id == id);
            _context.Todos.Remove(item);

            return Task.CompletedTask;
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            return await Task.Run(() => _context.Todos);
        }

        public Task ToggleComplete(int id)
        {
            var item = _context.Todos.First(c => c.Id == id);
            item.Completed = !item.Completed;

            return Task.CompletedTask;
        }
    }
}
