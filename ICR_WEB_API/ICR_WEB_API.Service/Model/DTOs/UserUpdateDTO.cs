using System.ComponentModel.DataAnnotations;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
