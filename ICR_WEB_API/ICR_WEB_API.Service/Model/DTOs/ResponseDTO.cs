namespace ICR_WEB_API.Service.Model.DTOs
{
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
        public string ImageLicensePlate { get; set; }
        public bool IsAnswerSubmitted { get; set; }

        // Navigation Property
        public int UserId { get; set; }
    }
}
