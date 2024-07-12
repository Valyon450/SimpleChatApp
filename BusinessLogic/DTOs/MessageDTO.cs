namespace DataAccess.Entities
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required int ChatId { get; set; }
        public required string ChatName { get; set; }
        public required int UserId { get; set; }
        public required string UserName { get; set; }
    }
}
