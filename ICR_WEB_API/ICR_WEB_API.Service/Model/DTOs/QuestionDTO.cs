using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public bool IsShowable { get; set; }
        public float SortOrder { get; set; }
        public List<OptionDTO>? Options { get; set; }
        public List<RatingScaleItemDTO>? RatingScaleItems { get; set; }
    }
}
