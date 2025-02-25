using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Entity
{
    public class AnswerOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AnswerId { get; set; }

        [Required]
        public int OptionId { get; set; }

        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }

        [ForeignKey("OptionId")]
        public Option Option { get; set; }
    }
}
