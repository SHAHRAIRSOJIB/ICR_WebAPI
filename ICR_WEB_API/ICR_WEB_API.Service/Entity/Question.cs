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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public EnumCollection.QuestionType Type { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public bool IsMandatory { get; set; } = false;

        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
      
    }
}
