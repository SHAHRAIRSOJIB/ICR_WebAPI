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

        public int Id { get; set; }
        public int TargetQuestionId { get; set; }
        public int ParentQuestionId { get; set; }
        public string Operator { get; set; }
        public string ExpectedValue { get; set; }

        public Question TargetQuestion { get; set; }
        public Question ParentQuestion { get; set; }
    }


}
