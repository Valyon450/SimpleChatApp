namespace BusinessLogic.DTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int OwnerId { get; set; }
        public required string OwnerName { get; set; }
    }
}
