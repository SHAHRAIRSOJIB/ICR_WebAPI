using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Model
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserType? UserType;
        public string Token { get; set; }
    }
}
