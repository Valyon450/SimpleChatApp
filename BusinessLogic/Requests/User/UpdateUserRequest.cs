using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Requests.User
{
    public class UpdateUserRequest
    {
        [NotMapped]
        public int Id { get; set; }
        public string? UserName { get; set; }
    }
}
