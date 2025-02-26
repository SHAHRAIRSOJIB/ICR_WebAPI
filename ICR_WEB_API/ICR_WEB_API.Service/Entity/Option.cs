using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Entity
{
    public class Option // Checkbox (multiple choice), Select (single)
    {
        public int Id { get; set; }
        public string OptionText { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }

}
