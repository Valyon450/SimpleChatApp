using BusinessLogic.Requests.Chat;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services.Interfaces
{
    public interface IChatService : ICRUD<ChatDTO, CreateChatRequest, UpdateChatRequest>
    {
        Task<IEnumerable<UserDTO>> GetAllChatMembersAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<MessageDTO>> GetAllChatMessagesAsync(int id, CancellationToken cancellationToken);
        Task AddUserToChatAsync(AddUserToChatRequest requestObject, CancellationToken cancellationToken);
        Task RemoveUserFromChatAsync(RemoveUserFromChatRequest requestObject, CancellationToken cancellationToken);
    }
}
