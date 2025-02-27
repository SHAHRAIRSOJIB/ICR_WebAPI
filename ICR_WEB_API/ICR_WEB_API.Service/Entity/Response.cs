namespace ICR_WEB_API.Service.Entity
{
    public class Response
    {
        public int Id { get; set; }
        public DateTime SubmissionDate { get; set; }

        // Navigation Property
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
