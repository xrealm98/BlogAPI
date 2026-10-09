using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required]
        [MinLength(4, ErrorMessage = "Name must be 4 characters")]
        [MaxLength(100, ErrorMessage = "Name cannot be over 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [MinLength(4, ErrorMessage = "Slug must be 4 characters")]
        public string Slug { get; set; } = string.Empty;
    }
}