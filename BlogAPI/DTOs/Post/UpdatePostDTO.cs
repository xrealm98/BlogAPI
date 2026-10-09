using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DTOs.Post
{
    public class UpdatePostDTO
    {
        [Required]
        [MinLength(4, ErrorMessage = "Title must be 4 characters")]
        [MaxLength(100, ErrorMessage = "Title cannot be over 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(4, ErrorMessage = "Content must be 4 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }
    }
}