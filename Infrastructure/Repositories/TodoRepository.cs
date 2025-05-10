using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Enums;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Todo>> GetAllAsync(string? status = null, string? priority = null, DateTime? startDate = null, DateTime? endDate = null, Expression<Func<Todo, object>>? orderBy = null, string? sortOrder = "asc")
    {
        var query = _context.Todos.AsNoTracking();

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<TodoStatus>(status, out var parsedStatus))
            query = query.Where(t => t.Status == parsedStatus);

        if (!string.IsNullOrEmpty(priority) && Enum.TryParse<TodoPriority>(priority, out var parsedPriority))
            query = query.Where(t => t.Priority == parsedPriority);

        if (startDate.HasValue)
            query = query.Where(t => t.CreatedDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(t => t.CreatedDate <= endDate.Value);
        if (orderBy!= null)
        {
            if (!string.IsNullOrWhiteSpace(sortOrder) && sortOrder.ToLower() == "asc")
            {
                query = query.OrderBy(orderBy);
            }
            query = query.OrderByDescending(orderBy);
        }
        return await query.ToListAsync();
    }

    public async Task<Todo?> GetByIdAsync(Guid id) => await _context.Todos.FindAsync(id);

    public async Task AddAsync(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Todo todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return;
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }
}