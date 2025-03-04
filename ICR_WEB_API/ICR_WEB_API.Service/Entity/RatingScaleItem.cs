namespace ICR_WEB_API.Service.Entity
{
    public class RatingScaleItem
    {
        public int Id { get; set; }
        public required string ItemText { get; set; }

        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }
    }
}
