using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICR_WEB_API.Service.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICR_WEB_API.Service.Entity
{
    public class Question
    {
        public int Id { get; set; }

        public EnumCollection.QuestionType Type { get; set; }

        public string QuestionText { get; set; }

        public bool IsMandatory { get; set; } = false;
      
    }
}
