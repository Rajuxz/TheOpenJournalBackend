using TheOpenJournal.Data;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Repository.Interfaces;

namespace TheOpenJournal.Repository.Implementations
{
    public class TagRepository:Repository<Tag>,ITagRepository
    {
        internal readonly ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context):base(context)
        {
            _context = context;            
        }
    }
}
