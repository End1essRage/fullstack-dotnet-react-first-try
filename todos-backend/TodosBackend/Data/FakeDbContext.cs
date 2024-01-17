using TodosBackend.Models;

namespace TodosBackend.Data
{
    public class FakeDbContext
    {
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
