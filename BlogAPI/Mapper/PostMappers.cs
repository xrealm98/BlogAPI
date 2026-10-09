using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DTOs.Post;
using BlogAPI.Models;

namespace BlogAPI.Mapper
{
    public static class PostMappers
    {
        public static Post CreatePostDtoToPostModel(this CreatePostDTO postDto)
        {
            return new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                CategoryId = postDto.CategoryId
            };
        }

        public static Post UpdatePostDtoToPostModel (this UpdatePostDTO postDto)
        {
            return new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                CategoryId = postDto.CategoryId
            };
        }
        public static ResponsePostDTO ToResponsePostDto (this Post postModel)
        {
            return new ResponsePostDTO
            {
                Id = postModel.Id,
                Title = postModel.Title,
                Content = postModel.Content,
                CreationDate = postModel.CreationDate,
                ModifiedDate = postModel.ModifiedDate,
                AuthorId = postModel.User.Id,
                AuthorName = postModel.User.Name,
                CategoryId = postModel.CategoryId,
                CategoryName = postModel.Category.Name
            };
        }

    
    }
}