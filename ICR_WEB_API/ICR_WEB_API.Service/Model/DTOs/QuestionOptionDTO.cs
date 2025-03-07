using ICR_WEB_API.Service.Entity;
using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class QuestionOptionDTO
    {
        public int Id { get; set; }

        public QuestionType Type { get; set; }

        public string QuestionText { get; set; }

        public bool IsMandatory { get; set; } = false;
        public List<Option> Options { get; set; }
    }
}
