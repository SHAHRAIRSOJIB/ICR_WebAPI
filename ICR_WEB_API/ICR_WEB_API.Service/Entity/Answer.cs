using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Entity
{
    public class Answer
    {
        public int Id { get; set; }
        public EnumCollection.QuestionType Type { get; set; }
        public string QuestionText { get; set; }

        public ICollection<Option> Options { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
