namespace BusinessLogic.DTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int CreatedById { get; set; }
        public required string CreatorName { get; set; }
    }
}
