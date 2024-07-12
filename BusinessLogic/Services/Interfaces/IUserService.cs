using BusinessLogic.Requests.User;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services.Interfaces
{
    public interface IUserService : ICRUD<UserDTO, CreateUserRequest, UpdateUserRequest>
    {
    }
}
