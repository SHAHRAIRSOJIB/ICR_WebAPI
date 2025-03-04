using ICR_WEB_API.Service.Enum;

namespace ICR_WEB_API.Service.Entity
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public EnumCollection.UserType? UserType { get; set; }
    }
}
