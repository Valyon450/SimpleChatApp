using BusinessLogic.Requests.User;
using DataAccess.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IUserService : ICRUD<UserDTO, CreateUserRequest, UpdateUserRequest>
    {
    }
}
