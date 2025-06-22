namespace FilmZone.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext context;
        private DbSet<T> dbset;
        public Repository(ApplicationDbContext Context)
        {
            context = Context;
            dbset = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return dbset;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }
        public T GetById(int id)
        {
            return dbset.Find(id);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(id);
        }
        public async Task<T> GetByCompositeIdAsync(int id1,int id2)
        {
            return await dbset.FindAsync(id1,id2);
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
        }
        public void Update(T entity)
        {
            dbset.Update(entity);
        }
        public async Task UpdateAsync(T entity)
        {
            //   await dbset.Update(entity);
        }
        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public async Task DeleteAsyncById(int id)
        {
            var entity = await dbset.FindAsync(id);
            if (entity != null) { dbset.Remove(entity); }


        }
        public async Task SaveChangesAsync() 
        {
            await context.SaveChangesAsync();
        }
    }
}
