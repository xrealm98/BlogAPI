using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.DTOs.Auth
{
    public class ResponseAuthDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}