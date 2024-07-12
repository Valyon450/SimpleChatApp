using BusinessLogic.Requests.Message;
using DataAccess.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IMessageService : ICRUD<MessageDTO, CreateMessageRequest, UpdateMessageRequest>
    {
    }
}
