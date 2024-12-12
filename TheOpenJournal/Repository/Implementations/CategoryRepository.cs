using TheOpenJournal.Data;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Repository.Interfaces;

namespace TheOpenJournal.Repository.Implementations
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        internal readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
