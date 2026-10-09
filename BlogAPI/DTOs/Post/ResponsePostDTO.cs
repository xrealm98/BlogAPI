using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DTOs.Post
{
    public class ResponsePostDTO
    {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string AuthorId { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    }
}