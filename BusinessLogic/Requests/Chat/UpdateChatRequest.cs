using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Requests.Chat
{
    public class UpdateChatRequest
    {
        [NotMapped]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
