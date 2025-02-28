using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICR_WEB_API.Service.Enum.EnumCollection;

namespace ICR_WEB_API.Service.Model.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<OptionDTO>? Options { get; set; }
        public List<RatingScaleItemDTO>? RatingScaleItems{ get; set; }
    }
    public class OptionDTO
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
    }
    public class RatingScaleItemDTO
    {
        public int Id { get; set; }
        public string ItemText { get; set; }
    }
}
