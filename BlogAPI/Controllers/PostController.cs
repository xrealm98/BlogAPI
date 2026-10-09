using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.DTOs.Post;
using BlogAPI.Helpers;
using BlogAPI.Mapper;
using BlogAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
       
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObj queryObj)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var posts = await _service.GetAllAsync(queryObj);

            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
             if(!ModelState.IsValid) return BadRequest(ModelState);
             
            var post = await _service.GetByIdAsync(id);

            if(post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO postDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(userId == null) return Unauthorized();

            var createdPost = await _service.CreatePostAsync(postDto, userId);

            return CreatedAtAction(nameof(GetById), new { id = createdPost.Id }, createdPost);
           
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostDTO updatePostDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isAdmin = User.IsInRole("Admin");

            try
            {
                var post = await _service.UpdatePostAsync(id, updatePostDto, userId!, isAdmin);
                if(post == null) return NotFound();
                return Ok(post);
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message); // Forbidden
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
             if(!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isAdmin = User.IsInRole("Admin");

            try
            {
                var post = await _service.DeletePostAsync(id, userId!, isAdmin);
                if(!post) return NotFound();
                return NoContent();
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
        }
       
    }
}