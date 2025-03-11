namespace TheOpenJournal.Services.Interfaces
{
    public interface ILikeService
    {
        Task<bool> LikePost(string userId, string postId);
    }
}
