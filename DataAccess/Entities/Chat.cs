namespace DataAccess.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int CreatedById { get; set; }
        public required User CreatedBy { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<UserChat>? UserChats { get; set; }
    }
}
