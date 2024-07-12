using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Requests.Chat;
using BusinessLogic.Requests.Message;
using BusinessLogic.Requests.User;
using DataAccess.Entities;

namespace BusinessLogic.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Chat, ChatDTO>()
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.CreatedBy.Id))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.CreatedBy.UserName));
            CreateMap<CreateChatRequest, Chat>();
            CreateMap<UpdateChatRequest, Chat>();

            CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.ChatName, opt => opt.MapFrom(src => src.Chat.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<CreateMessageRequest, Message>();
            CreateMap<UpdateMessageRequest, Message>();

            CreateMap<User, UserDTO>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
