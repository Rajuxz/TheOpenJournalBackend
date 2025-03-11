using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class LikeService : ILikeService
    {
        public Task<bool> LikePost(string userId, string postId)
        {
            return Task.FromResult(true);
        }
    }
}
