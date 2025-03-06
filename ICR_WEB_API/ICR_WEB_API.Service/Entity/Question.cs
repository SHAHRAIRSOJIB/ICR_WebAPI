using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Entity
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsShowable { get; set; }
        public float SortOrder { get; set; }
        public QuestionType Type { get; set; }

        // Relationships (only populated based on question type)

        public virtual ICollection<Option>? Options { get; set; }      // For Select/Checkbox

        public virtual ICollection<RatingScaleItem>? RatingScaleItems { get; set; } // For Rating questions

    }
}
