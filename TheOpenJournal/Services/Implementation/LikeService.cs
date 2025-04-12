using TheOpenJournal.Models.Domain;
using TheOpenJournal.Repository.Interfaces;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class LikeService : ILikeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ILikeRepository _likeRepository;
        public LikeService(IUserRepository userRepository,
            IPostRepository postRepository,
            ILikeRepository likeRepositry)
        {
            _userRepository = userRepository;        
            _postRepository = postRepository;
            _likeRepository = likeRepositry;

        }
        public async Task<bool> LikePostAsync(string userId, Guid postId)
        {
            string id = await _userRepository.GetIdByEmailAsync(userId);
            var post = await _postRepository.GetByIdAsync(postId);
            if(id == null)
            {
                throw new Exception("User not found.");
            }

            if(post == null)
            {
                throw new Exception("Post not found.");
            }

            //corner case, If user is trying to like one post for multiple time?
            bool hasLiked = await _likeRepository.IsPostLikedAsync(id, post.Id);
            if (!hasLiked)
            {

                var like = new Like
                {
                    UserId = userId,
                    PostId = postId,
                };

                int status = await _likeRepository.AddAsync(like);
                if (status > 0)
                {
                    return true;
                }
            }
            throw new Exception("Cannot like same post twice");
        }
    }
}
