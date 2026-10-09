using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.DTOs.Category;
using BlogAPI.Models;
using BlogAPI.Services.Post.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        
        public CategoryController(ICategoryService service)
        {   
          _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
             if(!ModelState.IsValid) return BadRequest(ModelState);
            var categories = await _service.GetCategoriesAsync();

            return Ok(categories);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
             if(!ModelState.IsValid) return BadRequest(ModelState);
            var category = await _service.GetCategoryByIDAsync(id);

            if(category == null) return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDto)
        {
             if(!ModelState.IsValid) return BadRequest(ModelState);
             var categoryModel = await _service.CreateCategoryAsync(categoryDto);
             
             return CreatedAtAction(nameof(GetCategoryById), new {id = categoryModel.Id}, categoryModel);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDTO categoryDto)
        {
             if(!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _service.UpdateCategoryAsync(id, categoryDto);
            if(category == null) return NotFound();
    
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
             if(!ModelState.IsValid) return BadRequest(ModelState);
            var categoryModel = await _service.DeleteCategoryAsync(id);
            if(!categoryModel) return NotFound();
            
            return NoContent();  
        }


        
    }
}