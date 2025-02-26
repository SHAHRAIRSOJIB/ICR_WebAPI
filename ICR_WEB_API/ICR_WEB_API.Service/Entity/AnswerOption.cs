using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Entity
{
    public class AnswerOption // Checkbox, Select
    {
        public int Id { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }

        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
