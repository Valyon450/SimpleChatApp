namespace BusinessLogic.Requests.Message
{
    public class CreateMessageRequest
    {
        public string? Text { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
    }
}
