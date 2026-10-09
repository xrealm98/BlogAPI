using Microsoft.AspNetCore.Identity;

namespace BlogAPI.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
