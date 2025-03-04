using System.ComponentModel.DataAnnotations;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class ResetPasswordDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
