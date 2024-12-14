using Microsoft.EntityFrameworkCore;
using TheOpenJournal.Data;
using TheOpenJournal.Repository.Interfaces;

namespace TheOpenJournal.Repository.Implementations
{
    public class Repository<T> : IRepository<T>, IDisposable where T: class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _database;
        private bool _disposed = false;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _database = _context.Set<T>();            
        }

        public async Task<int> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            return await SaveAsync();
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _database.FindAsync(id);
        }
        public  async Task<int> UpdateAsync(T entity)
        {
            _database.Update(entity);
            return await SaveAsync();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            _database.Remove(entity);
            return await SaveAsync();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _database.AsQueryable();
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> entity)
        {
            await _database.AddRangeAsync(entity);
            return await SaveAsync();
        }
        public void Dispose()
        {
            //Disposing is called
            // To indicate this method is being called explicitely
            // Dispose(false) is called by garbage collector itself.
            Dispose(true);
            // This indicate GC that the resource is already cleaned
            // And no need to call distructor.
            GC.SuppressFinalize(this);
        }


        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        //Finalizer or DESTRUCTOR
        ~Repository()
        {
            Dispose(false);
        }
    }
}
