using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Category;
using BlogAPI.Models;

namespace BlogAPI.Mapper
{
    public static class CategoryMappers
    {
        public static Category  CreateCategoryToModel(this CreateCategoryDTO createCategoryDTO)
        {
            return new Category
            {
                Name = createCategoryDTO.Name,
                Slug = createCategoryDTO.Slug
            };
            
        }

        public static Category  UpdateCategoryToModel(this UpdateCategoryDTO updateCategoryDTO)
        {
            return new Category
            {
                Name = updateCategoryDTO.Name,
                Slug = updateCategoryDTO.Slug
            };
            
        }

        public static ResponseCategoryDTO CategoryModelToResponseDto(this Category category)
        {
            return new ResponseCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            };
        }
    }
}