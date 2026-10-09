using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Post;
using BlogAPI.Helpers;
using BlogAPI.Models;

namespace BlogAPI.Repositories.Interfaces
{
    public interface IPostService
    {
        Task<List<ResponsePostDTO>> GetAllAsync(QueryObj queryObj);

        Task<ResponsePostDTO?> GetByIdAsync(int id);

        Task<ResponsePostDTO> CreatePostAsync(CreatePostDTO postDTO, string userId);

        Task<ResponsePostDTO?> UpdatePostAsync(int id, UpdatePostDTO postDto, string userId, bool isAdmin);

        Task<bool> DeletePostAsync(int id, string userId, bool isAdmin);

    }
}