using TheOpenJournal.Data;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Repository.Interfaces;

namespace TheOpenJournal.Repository.Implementations
{
    public class PostRepository:Repository<Post>,IPostRepository
    {
        internal readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;            
        }
    }
}
