﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using App.Desafio.Blog.Domain.Interfaces;
using App.Desafio.Blog.Domain.Dtos.Requests;

namespace App.Desafio.Blog.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// Get All posts
        /// </summary>        
        /// <response code="200">Success register user.</response>
        /// <response code="400">Fail validation.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _blogService.GetAllPostsAsync();
            return ApiResponse(posts);
        }

        /// <summary>
        /// Get All posts
        /// </summary>        
        /// <response code="200">Success register user.</response>
        /// <response code="400">Fail validation.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("user")]

        public async Task<IActionResult> GetAllPostsByUser()
        {
            var posts = await _blogService.GetAllPostsByUserIdAsync(UserId);
            return ApiResponse(posts);
        }

        /// <summary>
        /// Get All posts
        /// </summary>        
        /// <response code="200">Success register user.</response>
        /// <response code="400">Fail validation.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] Guid id)
        {
            var post = await _blogService.GetAllPostsByUserIdAsync(id);
            return ApiResponse(post);
        }

        /// <summary>
        /// Create new post
        /// </summary>        
        /// <response code="200">Success create post.</response>
        /// <response code="400">Fail validation.</response>
        /// <response code="500">Internal server error.</response>

        [HttpPost]
        [Authorize]  
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {   
            var post = await _blogService.CreatePostAsync(request, UserId);
            var message = "Success. Post created.";
            return ApiResponse(message, post);
        }

        /// <summary>
        /// Create new post
        /// </summary>        
        /// <response code="200">Success create post.</response>
        /// <response code="400">Fail validation.</response>
        /// <response code="500">Internal server error.</response>
        /// 
        [HttpPut]
        [Authorize]  
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequest request)
        {            
            var post = await _blogService.UpdatePostAsync(request, UserId);
            var message = "Success. Post updated.";
            return ApiResponse(message, post);         
        }

        /// <summary>
        /// Create new post
        /// </summary>        
        /// <response code="200">Success create post.</response>
        /// <response code="400">Fail validation.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [Authorize]  
        public async Task<IActionResult> DeletePost(Guid id)
        {
            await _blogService.DeletePostAsync(id, UserId);
            return NoContent();
        }
    }

}
