using System.Security.Permissions;

namespace FilmZone.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        public Task<IEnumerable<T>> GetAllAsync();
        public T GetById(int id);
        public Task<T> GetByIdAsync(int id);
        public void Add(T entity);
        public Task AddAsync(T entity);
        public void Update(T entity);
        public Task UpdateAsync(T entity);
        public void Delete(T entity);
        public Task DeleteAsyncById(int id);
        public Task SaveChangesAsync();

    }
}
