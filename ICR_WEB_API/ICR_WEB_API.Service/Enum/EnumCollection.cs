using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Enum
{
    public class EnumCollection
    {
        public enum QuestionType
        {
            Select,
            Checkbox,
            Textbox
        }

        public enum UserType
        {
            Admin,
            User
        }
    }
}
