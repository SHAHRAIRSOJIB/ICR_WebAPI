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
