using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Models;
using BlogAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories.Implementations
{
  public class CategoryRepository : ICategoryRepository
  {
    private readonly ApplicationDBContext _context;
    public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

    public Task<List<Category>> GetCategoriesAsync()
    {
      return _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIDAsync(int id)
    {
      return await _context.Categories.FindAsync(id);

    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
      await _context.Categories.AddAsync(category);
      await _context.SaveChangesAsync();

      return category;
    }

    
    public async Task<Category?> UpdateCategoryAsync(int id, Category category)
    {
       var categoryModel = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == id); 
       if(categoryModel == null) return null;

       categoryModel.Name = category.Name;
       categoryModel.Slug = category.Slug;

       await _context.SaveChangesAsync();
       return categoryModel;
    }

    public async Task<Category?> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == id);
        if(category == null) return null;
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return category;
      
    }

  }
}