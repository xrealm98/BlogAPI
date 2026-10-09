using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Post;
using BlogAPI.Helpers;
using BlogAPI.Mapper;
using BlogAPI.Repositories.Interfaces;

namespace BlogAPI.Services.Post
{
  public class PostService : IPostService
  {
    private readonly IPostRepository _repo;
    public PostService(IPostRepository repo)
        {
            _repo = repo;
        }


    public async Task<List<ResponsePostDTO>> GetAllAsync(QueryObj queryObj)
    {
      var posts = await _repo.GetAllAsync(queryObj);

      var postsToDto = posts.Select(p => p.ToResponsePostDto()).ToList();

      return postsToDto;
    }

    public async Task<ResponsePostDTO?> GetByIdAsync(int id)
    {
      var post = await _repo.GetByIdAsync(id);
      if(post == null) return null;

      var postToDto = post.ToResponsePostDto();
      return postToDto;

    }

    public async Task<ResponsePostDTO> CreatePostAsync(CreatePostDTO postDTO, string userId)
    {
      var post = postDTO.CreatePostDtoToPostModel();
      post.UserId = userId;

      await _repo.CreatePostAsync(post);

      var createdPost = await _repo.GetByIdAsync(post.Id);

      return createdPost!.ToResponsePostDto();

    }
    public async Task<ResponsePostDTO?> UpdatePostAsync(int id, UpdatePostDTO postDto, string userId, bool isAdmin)
    {
      var existingPost = await _repo.GetByIdAsync(id);
      if(existingPost == null) return null;

       if(existingPost.UserId != userId && !isAdmin)
        throw new UnauthorizedAccessException("No tienes permisos para editar este post");

      var postModel = postDto.UpdatePostDtoToPostModel();
      var post = await _repo.UpdatePostAsync(id, postModel);

      if(post == null) return null;
      
      return post.ToResponsePostDto();
    }

    public async Task<bool> DeletePostAsync(int id, string userId, bool isAdmin)
    {

      var existingPost = await _repo.GetByIdAsync(id);
      if(existingPost == null) return false;

      if(existingPost.UserId != userId && !isAdmin)
      throw new UnauthorizedAccessException("No tienes permisos para borrar este post");

      var post = await _repo.DeletePostAsync(id);
      
      return post != null;
    }
  }
}