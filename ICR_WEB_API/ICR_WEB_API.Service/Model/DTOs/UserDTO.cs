using System.ComponentModel.DataAnnotations;
using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
