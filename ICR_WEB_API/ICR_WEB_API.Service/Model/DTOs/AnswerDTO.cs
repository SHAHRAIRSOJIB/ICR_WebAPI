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

        public string ShopName { get; set; }                    // اسم المحل
        public string OwnerName { get; set; }                   // اسم المالك
        public string UnifiedLicenseNumber { get; set; }        // رقم الرخصة الموحد
        public string LicenseIssueDateLabel { get; set; }       // تاريخ اصدار الرخصة
        public string OwnerIDNumber { get; set; }               // رقم هوية المالك
        public string AIESECActivity { get; set; }              // نشاط الايزك
        public string Municipality { get; set; }                // البلدية
        public string FullAddress { get; set; }                 // العنوان الكامل

        // Navigation Property
        public int UserId { get; set; }
    }





}
