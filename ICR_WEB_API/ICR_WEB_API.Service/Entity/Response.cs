namespace ICR_WEB_API.Service.Entity
{
    public class Response
    {
        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; }

        // Shop/License Metadata
        public string ShopName { get; set; }                    // اسم المحل
        public string OwnerName { get; set; }                   // اسم المالك
        public string DistrictName { get; set; }
        public string StreetName { get; set; }
        public string UnifiedLicenseNumber { get; set; }        // رقم الرخصة الموحد
        public string LicenseIssueDateLabel { get; set; }       // تاريخ اصدار الرخصة
        public string OwnerIDNumber { get; set; }               // رقم هوية المالك
        public string AIESECActivity { get; set; }              // نشاط الايزك
        public string Municipality { get; set; }                // البلدية
        public string FullAddress { get; set; }                 // العنوان الكامل
        public string ImageBase64 { get; set; }
        public bool IsSubmited { get; set; }

        // Navigation Property
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
