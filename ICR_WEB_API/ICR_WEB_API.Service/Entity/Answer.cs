namespace ICR_WEB_API.Service.Entity
{
    public class Answer
    {
        public int Id { get; set; }

        // Foreign Keys
        public int ResponseId { get; set; }
        public int QuestionId { get; set; }

        /// <summary>
        /// Answer Data (populated based on question type)
        /// 
        /// SelectedOptionId, (RatingItemId, RatingValue) and TextResponse are optional
        /// but only one require depending on the question type
        /// 
        /// If question is Select/Checkbox type then only SelectedOptionId is require
        /// others are not allowed to entry
        /// 
        /// If question is Rating type then only RatingItemId and RatingValue is require
        /// others are not allowed to entry
        /// 
        /// If question is Input text type then only TextResponse is require
        /// others are not allowed to entry
        /// 
        /// ######################################
        /// #############    Note    #############
        /// ######################################
        /// 
        /// If the question type is checkbox then ResponseId and QuestionId will be same
        /// but the SelectedOptionId will be different
        /// </summary>

        // Select/Checkbox Type
        public int? SelectedOptionId { get; set; }

        // Rating Type
        public int? RatingItemId { get; set; }
        public int? RatingValue { get; set; } // 1-5 for Rating questions

        // Text Type
        public string? TextResponse { get; set; }

        // Navigation Properties
        public virtual Response? Response { get; set; }
        public virtual Question? Question { get; set; }
        public virtual Option? SelectedOption { get; set; }
        public virtual RatingScaleItem? RatingItem { get; set; }
    }
}


#region ResponseSchema
/*
[
  // Question type is Select
  {
    "responseId": 1,
    "questionId": 1,
    "selectedOptionId": 1
  },

  // Question type is Checkbox
  {
    "responseId": 1,
    "questionId": 2,
    "selectedOptionId": 2
  },
  {
    "responseId": 1,
    "questionId": 2,
    "selectedOptionId": 3
  },

  // Question type is Rating
  {
    "responseId": 1,
    "questionId": 3,
	"RatingItemId": 34,
	"RatingValue": 4
  },

  // Question type is Text
  {
    "responseId": 1,
    "questionId": 4,
	"TextResponse": "Kichu ekta"
  }
]
*/
#endregion response