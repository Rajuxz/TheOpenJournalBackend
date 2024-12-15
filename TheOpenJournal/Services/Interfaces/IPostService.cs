using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Services.Interfaces
{
    public interface IPostService
    {
        Task<Post> AddPostAsync(PostDTO postDto);
    }
}
