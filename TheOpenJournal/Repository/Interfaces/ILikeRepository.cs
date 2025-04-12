using TheOpenJournal.Models.Domain;

namespace TheOpenJournal.Repository.Interfaces
{
    public interface ILikeRepository:IRepository<Like>
    {
        public Task<bool> IsPostLikedAsync(string userId, Guid PostId);
    }
}
