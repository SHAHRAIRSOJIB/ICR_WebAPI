using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class ResponseWithQuestionsAndAnswerDTO
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
        public ICollection<QuestionWithAnswersDTO> QuestionWithAnswers { get; set; }
    }

    public class QuestionWithAnswersDTO
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public QuestionType Type { get; set; }
        public string QuestionText { get; set; }
        public float SortOrder { get; set; }
        public string? SelectedOptionText { get; set; }
        public string? RatingItemText { get; set; }
        public string? RatingItemValue { get; set; }
        public string? TextResponseAnswer { get; set; }
    }
}
