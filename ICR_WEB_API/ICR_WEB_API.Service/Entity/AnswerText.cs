using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Entity
{
    public class AnswerText // for Text input
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
