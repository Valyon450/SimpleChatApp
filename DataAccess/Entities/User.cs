namespace DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public ICollection<Chat>? Chats { get; set; }
        public ICollection<UserChat>? UserChats { get; set; }
    }
}
