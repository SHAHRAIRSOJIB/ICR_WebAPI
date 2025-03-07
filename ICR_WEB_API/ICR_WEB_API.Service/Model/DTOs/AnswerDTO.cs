namespace ICR_WEB_API.Service.Model.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public int ResponseId { get; set; }
        public int QuestionId { get; set; }

        public int? SelectedOptionId { get; set; }

        public int? RatingItemId { get; set; }
        public string? RatingValue { get; set; }

        public string? TextResponse { get; set; }

        public QuestionDTO? Question { get; set; }
        public ResponseDTO? Response { get; set; }
    }
}
