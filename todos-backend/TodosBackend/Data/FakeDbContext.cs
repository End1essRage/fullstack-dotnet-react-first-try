namespace TodosBackend.Data
{
    public class FakeDbContext
    {
        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
