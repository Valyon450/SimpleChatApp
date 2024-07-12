using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Requests.Message
{
    public class UpdateMessageRequest
    {
        [NotMapped]
        public int Id { get; set; }
        public string? Text { get; set; }
    }
}
