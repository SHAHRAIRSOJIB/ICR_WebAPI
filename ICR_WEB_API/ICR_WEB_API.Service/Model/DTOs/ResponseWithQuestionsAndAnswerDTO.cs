using ICR_WEB_API.Service.Entity;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class FormattedResponseDto
    {
        public List<ColumnDefinition> Columns { get; set; }
        public List<FormattedResponseRowDto> Rows { get; set; }
    }

    public class FormattedResponseRowDto
    {
        public int ResponseId { get; set; }
        public DateTime SubmissionDate { get; set; }
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
        public string ImageLicensePlate { get; set; }
        public bool IsAnswerSubmitted { get; set; }
        public User User { get; set; }
        public Dictionary<string, string> Answers { get; set; }
    }

    public class ColumnDefinition
    {
        public int QuestionId { get; set; }
        public int? OptionId { get; set; } // For select/checkbox types
        public int? RatingItemId { get; set; } // For rating type
        public string UniqueKey { get; set; } // e.g., "1-What is your hobby? - 5-Reading"
        public string DisplayLabel { get; set; } // e.g., "What is your hobby? - Reading"
    }
}
