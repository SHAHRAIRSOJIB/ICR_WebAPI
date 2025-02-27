namespace ICR_WEB_API.Service.Entity
{
    public class Answer
    {
        public int Id { get; set; }

        // Foreign Keys
        public int ResponseId { get; set; }
        public int QuestionId { get; set; }

        // Answer Data (populated based on question type)
        public int? SelectedOptionId { get; set; } // For Select/Checkbox (store Option.Id)
        public int? RatingItemId { get; set; } // For Rating questions (store RatingScaleItem.Id)
        public int? RatingValue { get; set; } // 1-5 for Rating questions
        public string TextResponse { get; set; } // For Text questions

        // Navigation Properties
        public virtual Response Response { get; set; }
        public virtual Question Question { get; set; }
        public virtual Option SelectedOption { get; set; }
        public virtual RatingScaleItem RatingItem { get; set; }
    }
}
