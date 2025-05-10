using System.Linq.Expressions;
using TodoManagementAPI.Application.DTOs;
using TodoManagementAPI.Domain.Entities;

namespace TodoManagementAPI.Application.Services.Interface;

public interface ITodoService
{
    Task<List<TodoDto>> GetAllTodosAsync(string? status = null, string? priority = null, DateTime? startDate = null, DateTime? endDate = null, string? orderBy = "Title" , string? sortOrder = "asc");
    Task<TodoDto?> GetTodoByIdAsync(Guid id);
    Task<TodoDto> CreateTodoAsync(TodoDto todoDto);
    Task UpdateTodoAsync(Guid id, TodoDto todoDto);
    Task DeleteTodoAsync(Guid id);
    Task MarkAsCompleteAsync(Guid id);
}