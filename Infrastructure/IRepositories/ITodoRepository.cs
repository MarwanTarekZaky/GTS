using System.Linq.Expressions;
using Domain.Entities;

namespace Infrastructure.IRepositories;

public interface ITodoRepository
{
    Task<List<Todo>> GetAllAsync(string? status = null, string? priority = null, DateTime? startDate = null, DateTime? endDate = null, Expression<Func<Todo, object>>? orderBy = null, string? sortOrder = "asc");
    Task<Todo?> GetByIdAsync(Guid id);
    Task AddAsync(Todo todo);
    Task UpdateAsync(Todo todo);
    Task DeleteAsync(Guid id);
}