using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Entity
{
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(255)]
        public string OptionText { get; set; }

        public int? SortOrder { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

    }

}
