using Microsoft.EntityFrameworkCore;
using TheOpenJournal.Data;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Repository.Interfaces;

namespace TheOpenJournal.Repository.Implementations
{
    public class LikeRepository:Repository<Like>,ILikeRepository
    {
        internal readonly ApplicationDbContext _context;
        public LikeRepository( ApplicationDbContext context):base(context)
        {
            _context = context;            
        }

        public async Task<bool> IsPostLikedAsync(string userId, Guid postId)
        {
            return await _context.Likes.AnyAsync(like=>like.UserId == userId && like.PostId == postId);
        }
    }
}
