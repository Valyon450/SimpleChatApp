using System.Net.Http.Json;
using BusinessLogic.DTOs;
using BusinessLogic.Requests.Chat;

namespace IntegrationTests
{
    public class ChatControllerTests : IntegrationTesting
    {
        public ChatControllerTests(CustomWebFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task CanGetAllChats()
        {
            var response = await client.GetAsync("/api/chat");
            response.EnsureSuccessStatusCode();

            var chats = await response.Content.ReadFromJsonAsync<List<ChatDTO>>();
            Assert.NotNull(chats);
        }

        [Fact]
        public async Task CanGetChatById()
        {
            var response = await client.GetAsync("/api/chat/1");
            response.EnsureSuccessStatusCode();

            var chat = await response.Content.ReadFromJsonAsync<ChatDTO>();
            Assert.NotNull(chat);
            Assert.Equal(1, chat.Id);
        }

        [Fact]
        public async Task CanGetAllChatMembers()
        {
            var response = await client.GetAsync("/api/chat/1/members");
            response.EnsureSuccessStatusCode();

            var members = await response.Content.ReadFromJsonAsync<List<UserDTO>>();
            Assert.NotNull(members);
        }

        [Fact]
        public async Task CanGetAllChatMessages()
        {
            var response = await client.GetAsync("/api/chat/1/messages");
            response.EnsureSuccessStatusCode();

            var messages = await response.Content.ReadFromJsonAsync<List<MessageDTO>>();
            Assert.NotNull(messages);
        }

        [Fact]
        public async Task CanSearchChats()
        {
            var response = await client.GetAsync("/api/chat/test/search");
            response.EnsureSuccessStatusCode();

            var chats = await response.Content.ReadFromJsonAsync<List<ChatDTO>>();
            Assert.NotNull(chats);
        }

        [Fact]
        public async Task CanCreateChat()
        {
            var newChat = new CreateChatRequest
            {
                Name = "Test Chat",
                OwnerId = 1,
            };

            var response = await client.PostAsJsonAsync("/api/chat", newChat);
            response.EnsureSuccessStatusCode();

            var createdChat = await response.Content.ReadFromJsonAsync<ChatDTO>();
            Assert.NotNull(createdChat);
            Assert.Equal(newChat.Name, createdChat.Name);
            Assert.Equal(newChat.OwnerId, createdChat.OwnerId);
        }

        [Fact]
        public async Task CanUpdateChat()
        {
            var updatedChat = new UpdateChatRequest
            {
                Id = 1,
                Name = "Updated Chat",
            };

            var response = await client.PutAsJsonAsync("/api/chat", updatedChat);
            response.EnsureSuccessStatusCode();

            var chat = await response.Content.ReadFromJsonAsync<ChatDTO>();
            Assert.NotNull(chat);
            Assert.Equal(updatedChat.Name, chat.Name);
        }

        [Fact]
        public async Task CanDeleteChat()
        {
            var response = await client.DeleteAsync("/api/chat/1?userId=1");
            response.EnsureSuccessStatusCode();

            var getResponse = await client.GetAsync("/api/chat/1");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Fact]
        public async Task CanAddUserToChat()
        {
            var addUserRequest = new AddUserToChatRequest
            {
                ChatId = 1,
                UserId = 2,
            };

            var response = await client.PatchAsJsonAsync("/api/chat/join", addUserRequest);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CanRemoveUserFromChat()
        {
            var removeUserRequest = new RemoveUserFromChatRequest
            {
                ChatId = 1,
                UserId = 2,
            };

            var response = await client.PatchAsJsonAsync("/api/chat/left", removeUserRequest);
            response.EnsureSuccessStatusCode();
        }
    }
}
