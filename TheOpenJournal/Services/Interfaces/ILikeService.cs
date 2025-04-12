namespace TheOpenJournal.Services.Interfaces
{
    public interface ILikeService
    {
        Task<bool> LikePostAsync(string userId, Guid postId);
    }
}
