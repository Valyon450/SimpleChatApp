using BusinessLogic.DTOs;
using BusinessLogic.Requests.Message;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Retrieves all messages.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of messages.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MessageDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var messages = await _messageService.GetAllAsync(cancellationToken);

            if (messages == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(messages);
            }            
        }

        /// <summary>
        /// Retrieves a specific message.
        /// </summary>
        /// <param name="id">Message Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>The message.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDTO>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _messageService.GetByIdAsync(id, cancellationToken);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="requestObject">Message data.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>Created message Id.</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMessageRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                int id = await _messageService.CreateAsync(requestObject, cancellationToken);

                return CreatedAtAction(nameof(GetById), new { id }, requestObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing message.
        /// </summary>
        /// <param name="requestObject">Updated message data.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateMessageRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                await _messageService.UpdateAsync(requestObject, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a message.
        /// </summary>
        /// <param name="id">Message Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _messageService.DeleteAsync(id, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
