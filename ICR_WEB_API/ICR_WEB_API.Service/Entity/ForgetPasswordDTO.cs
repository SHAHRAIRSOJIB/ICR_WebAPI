using ICR_WEB_API.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Entity
{
    public class ForgetPasswordDTO
    {
        public required string Email { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
        public required string ConfirmPassword { get; set; }


    }
}
