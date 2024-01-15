namespace TodosBackend.Data
{
    public class FakeTodosRepository : ITodoRepository
    {
        private FakeDbContext _context;
        public FakeTodosRepository(FakeDbContext context) {
            _context = context;
        }
        public Task AddTodo(string text)
        {
            _context.Todos.Add(new Todo { Text = text });
            return Task.CompletedTask;
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            return await Task.Run(() => _context.Todos);
        }
    }
}
