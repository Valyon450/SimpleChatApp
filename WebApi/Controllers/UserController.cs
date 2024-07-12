using BusinessLogic.DTOs;
using BusinessLogic.Requests.User;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of users.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(cancellationToken);

            if (users == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(users);
            }            
        }

        /// <summary>
        /// Retrieves a specific user.
        /// </summary>
        /// <param name="id">User Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>The user.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id, cancellationToken);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="requestObject">User data.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>Created user Id.</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                int id = await _userService.CreateAsync(requestObject, cancellationToken);

                return CreatedAtAction(nameof(GetById), new { id }, requestObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="requestObject">Updated user data.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest requestObject, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.UpdateAsync(requestObject, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">User Id.</param>
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
                await _userService.DeleteAsync(id, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
