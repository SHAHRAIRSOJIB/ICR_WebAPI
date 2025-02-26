using ICR_WEB_API.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_WEB_API.Service.Model
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public EnumCollection.UserType? UserType;
        public string Token { get; set; }
    }
}
