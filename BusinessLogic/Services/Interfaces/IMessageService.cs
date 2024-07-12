using BusinessLogic.Requests.Message;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services.Interfaces
{
    public interface IMessageService : ICRUD<MessageDTO, CreateMessageRequest, UpdateMessageRequest>
    {
    }
}
