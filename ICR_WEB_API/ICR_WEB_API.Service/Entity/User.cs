using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType? UserType { get; set; }
    }
}
