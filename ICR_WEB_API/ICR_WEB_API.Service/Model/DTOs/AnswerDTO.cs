using ICR_WEB_API.Service.Entity;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public int ResponseId { get; set; }
        public int QuestionId { get; set; }

        public int? SelectedOptionId { get; set; }

        public int? RatingItemId { get; set; }
        public int? RatingValue { get; set; }

        public string? TextResponse { get; set; }

        public QuestionDTO? Question { get; set; }
        public ResponseDTO? Response { get; set; }
    }

    public class ResponseDTO
    {
        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; }

        // Shop/License Metadata
        public string ShopName { get; set; }
        public string OwnerName { get; set; }
        public string DistrictName { get; set; }
        public string StreetName { get; set; }
        public string UnifiedLicenseNumber { get; set; }
        public string LicenseIssueDateLabel { get; set; }
        public string OwnerIDNumber { get; set; }
        public string AIESECActivity { get; set; }
        public string Municipality { get; set; }
        public string FullAddress { get; set; }
        public bool IsSubmited { get; set; }

        // Navigation Property
        public int UserId { get; set; }
    }
}
