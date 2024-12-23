using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Services.Interfaces
{
    public interface IPostService
    {
        Task<bool> AddPostAsync(PostDTO postDto);
        Task<List<GetPostDTO>> GetPostsAsync();
        Task<List<GetPostDTO>> GetPostsByCategoryAsync(Guid guid);
        Task<bool> UpdatePostAsync(UpdatePostDTO updatePostDto);
    }
}
