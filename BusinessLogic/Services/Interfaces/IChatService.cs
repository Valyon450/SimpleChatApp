using BusinessLogic.Requests.Chat;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services.Interfaces
{
    public interface IChatService : ICRUD<ChatDTO, CreateChatRequest, UpdateChatRequest>
    {
        Task AddUserToChatRequestAsync(AddUserToChatRequest requestObject, CancellationToken cancellationToken);
        Task RemoveUserFromChatRequestAsync(RemoveUserFromChatRequest requestObject, CancellationToken cancellationToken);
    }
}
