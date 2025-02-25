using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Entity
{
    public class Answer
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string QuestionText { get; set; }
        public bool IsMandatory { get; set; } = false;
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Option> Options { get; set; }
        public ICollection<QuestionCondition> TargetConditions { get; set; }
        public ICollection<QuestionCondition> ParentConditions { get; set; }

    }
}
