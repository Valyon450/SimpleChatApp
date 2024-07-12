using BusinessLogic.DTOs;
using BusinessLogic.Requests.Chat;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Retrieves all chats.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of chats.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ChatDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var chats = await _chatService.GetAllAsync(cancellationToken);

            if (chats == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(chats);
            }            
        }

        /// <summary>
        /// Retrieves a specific chat.
        /// </summary>
        /// <param name="id">Chat Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>The chat.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChatDTO>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var chat = await _chatService.GetByIdAsync(id, cancellationToken);

                return Ok(chat);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all members of the specific chat.
        /// </summary>
        /// <param name="id">Chat Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of users.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}/members")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserDTO>>> GetAllChatMembers(int id, CancellationToken cancellationToken)
        {
            try
            {
                var members = await _chatService.GetAllChatMembersAsync(id, cancellationToken);

                return Ok(members);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all messages of the specific chat.
        /// </summary>
        /// <param name="id">Chat Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of messages.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MessageDTO>>> GetAllChatMessages(int id, CancellationToken cancellationToken)
        {
            try
            {
                var messages = await _chatService.GetAllChatMessagesAsync(id, cancellationToken);

                return Ok(messages);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Search chats.
        /// </summary>
        /// <param name="searchQuery">Text search query.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of chats with matches by name.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{searchQuery}/search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ChatDTO>>> SearchChats(string searchQuery, CancellationToken cancellationToken)
        {
            var chats = await _chatService.SearchChatsAsync(searchQuery, cancellationToken);

            if (chats == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(chats);
            }
        }

        /// <summary>
        /// Creates a new chat.
        /// </summary>
        /// <param name="requestObject">Chat data.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>Created chat Id.</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                int id = await _chatService.CreateAsync(requestObject, cancellationToken);

                return CreatedAtAction(nameof(GetById), new { id }, requestObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing chat.
        /// </summary>
        /// <param name="requestObject">Updated chat data.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                await _chatService.UpdateAsync(requestObject, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a chat.
        /// </summary>
        /// <param name="id">Chat Id.</param>
        /// <param name="userId">Chat owner Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id, int userId, CancellationToken cancellationToken)
        {
            try
            {
                await _chatService.DeleteAsync(id, userId, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adds a user to the chat.
        /// </summary>
        /// <param name="requestObject">Chat and user data.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPatch("/join")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserToChat([FromBody] AddUserToChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                await _chatService.AddUserToChatAsync(requestObject, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Removes the user from the chat.
        /// </summary>
        /// <param name="requestObject">Chat and user data.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPatch("/left")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveUserFromChat([FromBody] RemoveUserFromChatRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                await _chatService.RemoveUserFromChatAsync(requestObject, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
