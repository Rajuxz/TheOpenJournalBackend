namespace TheOpenJournal.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        public IQueryable<T> GetQueryable();
        Task<int> SaveAsync();

    }
}
