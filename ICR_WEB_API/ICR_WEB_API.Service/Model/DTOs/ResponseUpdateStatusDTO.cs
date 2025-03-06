using System.ComponentModel.DataAnnotations;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class ResponseUpdateStatusDTO
    {
        [Required]
        public string UnifiedLicenseNumber { get; set; }
        [Required]
        public string OwnerIDNumber { get; set; }
        [Required]
        public bool IsAnswerSubmitted { get; set; }
    }
}
