using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Category;
using BlogAPI.Mapper;
using BlogAPI.Repositories.Interfaces;
using BlogAPI.Services.Post.Interface;

namespace BlogAPI.Services.Category
{
  public class CategoryService : ICategoryService
  {
   
    private readonly ICategoryRepository _repo;
    public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }


    public async Task<List<ResponseCategoryDTO>> GetCategoriesAsync()
    {
      var categories = await _repo.GetCategoriesAsync();
      var categoriesDto = categories.Select(cat => cat.CategoryModelToResponseDto()).ToList();

      return categoriesDto;
    }

    public async Task<ResponseCategoryDTO?> GetCategoryByIDAsync(int id)
    {
      var category = await _repo.GetCategoryByIDAsync(id);

      if(category == null) return null;

      return category.CategoryModelToResponseDto();
    }       

    public async Task<ResponseCategoryDTO> CreateCategoryAsync(CreateCategoryDTO categoryDto)
    {
      var categoryModel = await _repo.CreateCategoryAsync(categoryDto.CreateCategoryToModel());

      return categoryModel.CategoryModelToResponseDto();
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
       var categoryModel = await _repo.DeleteCategoryAsync(id); 
       
       return categoryModel != null;
    }

  

    public async Task<ResponseCategoryDTO?> UpdateCategoryAsync(int id, UpdateCategoryDTO category)
    {
      var categoryToModel = category.UpdateCategoryToModel();
      var categoryModel = await _repo.UpdateCategoryAsync(id, categoryToModel);

      if(categoryModel == null) return null;
      
      return categoryModel.CategoryModelToResponseDto();
    }
  }
}