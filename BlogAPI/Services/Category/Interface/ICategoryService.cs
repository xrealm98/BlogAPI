using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Category;
using BlogAPI.Models;

namespace BlogAPI.Services.Post.Interface
{
    public interface ICategoryService
    {
        Task<List<ResponseCategoryDTO>> GetCategoriesAsync();

        Task<ResponseCategoryDTO?> GetCategoryByIDAsync(int id);

        Task<ResponseCategoryDTO> CreateCategoryAsync(CreateCategoryDTO category);

        Task<ResponseCategoryDTO?> UpdateCategoryAsync(int id, UpdateCategoryDTO category);

        Task<bool> DeleteCategoryAsync(int id);

    }
}