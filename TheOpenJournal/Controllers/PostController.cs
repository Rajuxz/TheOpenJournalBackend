﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Controllers
{
    [Authorize]
    [Route("api/v1/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
       
        [HttpPost]
        public async Task<IActionResult> AddPost([FromForm] PostDTO postDto)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                postDto.User = userId;
                bool response = await _postService.AddPostAsync(postDto);
                if (response)
                {
                    return Ok(new { message = "Post added successfully" });
                }
                else
                {
                    return BadRequest(new { message = "Something went wrong. Please try again later." });
                }
            }
            else
            {
                return BadRequest(new { message = "Please provide all the data." });

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            var response = await _postService.GetPostsAsync();
            return Ok( new { response });
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetPostByCategory([FromRoute] string categoryId)
        {
            if (Guid.TryParse(categoryId, out Guid categoryGuid))
            {
                var response = await _postService.GetPostsByCategoryAsync(categoryGuid);
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = "Error, Invalid CategoryId found." });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePost([FromForm]UpdatePostDTO updatePostDto)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = await _postService.UpdatePostAsync(updatePostDto);
                if (isUpdated)
                {
                    return Ok(new {message="Post Update Successfully !"});
                }
                else
                {
                    return BadRequest(new { message = "Cannot Update Post !" });
                }
            }
            return BadRequest(new {message="Invalid Model State"});
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var posts = await _postService.GetPostByUserIdAsync(userId);
            return Ok(posts);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug([FromRoute]string slug)
        {
            var post = await _postService.GetPostBySlugAsync(slug);
            return Ok( new { post = post});
        }
    }
}
