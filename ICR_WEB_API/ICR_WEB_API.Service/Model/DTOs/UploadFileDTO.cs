using System.ComponentModel.DataAnnotations;
using ICR_WEB_API.Service.Model.Attributes;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class UploadFileDTO
    {
        [Required]
        public string Base64ImageString { get; set; }
        [Required]
        [Numeric(ErrorMessage = "Unified License Number must be numeric")]
        public string UnifiedLicenseNumber { get; set; }
    }
}
