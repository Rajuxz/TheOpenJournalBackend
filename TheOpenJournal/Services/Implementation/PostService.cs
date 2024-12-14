using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Repository.Interfaces;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class PostService:IPostService
    {
        private readonly IPostRepository _repository;
        public PostService(IPostRepository repository){ 
            _repository = repository;
        }

        public async Task<bool> AddPostAsync(PostDTO postDto)
        {
            return false;
        }
    }
}
