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
    public class QuestionCondition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TargetQuestionId { get; set; }

        [Required]
        public int ParentQuestionId { get; set; }

        [Required]
        [Column("Operator")] // Keeps the DB column name as "Operator" while avoiding conflicts in C#
        public string ConditionOperator { get; set; }

        [MaxLength(255)]
        public string ExpectedValue { get; set; }

        [ForeignKey("TargetQuestionId")]
        public Question TargetQuestion { get; set; }

        [ForeignKey("ParentQuestionId")]
        public Question ParentQuestion { get; set; }
    }
}
