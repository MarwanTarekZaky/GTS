using Microsoft.AspNetCore.Mvc;
using TodoManagementAPI.Application.Services.Interface;
using TodoManagementAPI.Application.DTOs;
using TodoManagementAPI.Shared.Exceptions;

namespace TodoManagementAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] string? status = null)
        {
            try
            {
                var todos = await _todoService.GetAllTodosAsync(status);
                return Ok(new { data = todos, message = "Todos retrieved successfully." });
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return StatusCode(500, new { message = "An error occurred while fetching todos.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(Guid id)
        {
            try
            {
                var todo = await _todoService.GetTodoByIdAsync(id);
                if (todo == null)
                    return NotFound(new { message = "Todo not found." });
                return Ok(new { data = todo, message = "Todo retrieved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the todo.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(TodoDto todoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values.SelectMany(v => v.Errors) });

            try
            {
                var createdTodo = await _todoService.CreateTodoAsync(todoDto);
                return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, new { data = createdTodo, message = "Todo created successfully." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = "Validation failed.", errors = ex.Errors });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the todo.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(Guid id, TodoDto updatedTodoDto)
        {
            try
            {
                await _todoService.UpdateTodoAsync(id, updatedTodoDto);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Todo not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the todo.", error = ex.Message });
            }
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkTodoAsComplete(Guid id)
        {
            try
            {
                await _todoService.MarkAsCompleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Todo not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while marking the todo as complete.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            try
            {
                await _todoService.DeleteTodoAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Todo not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the todo.", error = ex.Message });
            }
        }
    }
}
