namespace ICR_WEB_API.Service.Entity
{
    public class Option
    {
        public int Id { get; set; }
        public string OptionText { get; set; }

        // Foreign Key
        public int QuestionId { get; set; }

        public virtual Question? Question { get; set; }

    }

}
