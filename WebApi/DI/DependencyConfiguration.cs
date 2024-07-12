using BusinessLogic.Mappings;
using BusinessLogic.Requests.Chat;
using BusinessLogic.Requests.Message;
using BusinessLogic.Requests.User;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Validation.Services;
using BusinessLogic.Validation.Services.Interfaces;
using BusinessLogic.Validation.Validators.Chat;
using BusinessLogic.Validation.Validators.Message;
using BusinessLogic.Validation.Validators.User;
using DataAccess;
using DataAccess.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DI
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection ConfigureDependency(this IServiceCollection services, IConfiguration configuration)
        {
            // Database context
            services.AddDbContext<ISimpleChatDbContext, SimpleChatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // AutoMapper
            services.AddAutoMapper(typeof(AutomapperProfile));

            // Services
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();

            // Validators
            services.AddScoped<IValidator<CreateChatRequest>, CreateChatValidator>();
            services.AddScoped<IValidator<UpdateChatRequest>, UpdateChatValidator>();
            services.AddScoped<IValidator<AddUserToChatRequest>, AddUserToChatValidator>();
            services.AddScoped<IValidator<RemoveUserFromChatRequest>, RemoveUserFromChatValidator>();

            services.AddScoped<IValidator<CreateMessageRequest>, CreateMessageValidator>();
            services.AddScoped<IValidator<UpdateMessageRequest>, UpdateMessageValidator>();

            services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidator>();
            services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserValidator>();

            // Validation services
            services.AddScoped<IChatValidationService, ChatValidationService>();
            services.AddScoped<IMessageValidationService, MessageValidationService>();
            services.AddScoped<IUserValidationService, UserValidationService>();

            return services;
        }
    }
}
