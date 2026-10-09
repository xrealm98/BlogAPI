using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.DTOs.Post;
using BlogAPI.Helpers;
using BlogAPI.Models;
using BlogAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repositories.Implementations
{
  public class PostRepository : IPostRepository
  {
    private readonly ApplicationDBContext _context;
    
    public PostRepository(ApplicationDBContext context)
    {
        _context = context;
    }

     public async Task<List<Post>> GetAllAsync(QueryObj queryObj)
    {
        var posts = _context.Posts.Include(p => p.User).Include(p => p.Category).AsQueryable();

        if (queryObj.CategoryId.HasValue)
        {
            posts = posts.Where(post => post.CategoryId == queryObj.CategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(queryObj.AuthorName))
        {
            posts = posts.Where(post => post.User.Name.Contains(queryObj.AuthorName));
        }

        var skipElement = (queryObj.Page - 1) * queryObj.PageSize;

        return await posts.Skip(skipElement).Take(queryObj.PageSize).ToListAsync();
    }

    
    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts.Include(p => p.User).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }



    public async Task<Post> CreatePostAsync(Post postModel)
    {
       await _context.Posts.AddAsync(postModel);
       await _context.SaveChangesAsync();
      
       return postModel;
    }

    public async Task<Post?> UpdatePostAsync(int id, Post post)
    {
      var postModel = await _context.Posts.Include(p => p.User).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

      if(postModel == null)
        {
            return null;
                
        }
    
        postModel.Title = post.Title;
        postModel.Content = post.Content;
        postModel.CategoryId = post.CategoryId;
        postModel.ModifiedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        await _context.Entry(postModel).Reference(p => p.Category).LoadAsync();
        await _context.Entry(postModel).Reference(p => p.User).LoadAsync();

        return postModel;

    
    }

    public async Task<Post?> DeletePostAsync(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return null;
        }
        
        _context.Posts.Remove(post); 
        await _context.SaveChangesAsync();

        return post;
    }

}
}