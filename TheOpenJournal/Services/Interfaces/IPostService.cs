using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Services.Interfaces
{
    public interface IPostService
    {
        Task<bool> AddPostAsync(PostDTO postDto);
        Task<List<PostDTO>> GetPostsAsync();
    }
}
