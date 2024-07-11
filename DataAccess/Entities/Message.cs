namespace DataAccess.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required int ChatId { get; set; }
        public required Chat Chat { get; set; }
        public required int UserId { get; set; }
        public required User User { get; set; }
    }
}
