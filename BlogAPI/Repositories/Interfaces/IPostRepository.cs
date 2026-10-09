using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Post;
using BlogAPI.Helpers;
using BlogAPI.Models;

namespace BlogAPI.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync(QueryObj queryObj);

        Task<Post?> GetByIdAsync(int id);

        Task<Post> CreatePostAsync(Post postModel);

        Task<Post?> UpdatePostAsync(int id, Post post);

        Task<Post?> DeletePostAsync(int id);

    }
}