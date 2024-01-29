using TodosBackend.Data.Models;

namespace TodosBackend.Data.DataAccess.Abstractions
{
    public interface IRepository <T> where T : BaseModel
    {
        T Create(T entity);
        void Delete(T entity);
        T Update(T entity);
        Task<T> GetOneAsync(int id);
        Task<IEnumerable<T>> GetSomeAsync(int count);
        Task<IEnumerable<T>> GetPagedAsync(int page, int size);
        Task SaveChangesAsync();
    }
}
