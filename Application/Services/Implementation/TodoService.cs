using AutoMapper;
using TodoManagementAPI.Application.DTOs;
using TodoManagementAPI.Application.Services.Interface;
using TodoManagementAPI.Domain.Entities;
using TodoManagementAPI.Domain.Entities.Enums;
using TodoManagementAPI.Infrastructure.IRepositories;
using TodoManagementAPI.Shared.Exceptions;
using System;
using System.Linq.Expressions;
using TodoManagementAPI.Domain.Events;

namespace TodoManagementAPI.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventDispatcher _dispatcher;
        public TodoService(ITodoRepository repository, IMapper mapper, IDomainEventDispatcher dispatcher)
        {
            _repository = repository;
            _mapper = mapper;
            _dispatcher = dispatcher;
        }
        public async Task MarkAsCompleteAsync(Guid id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null) throw new Exception("Todo not found");

            todo.MarkAsCompleted(); // Triggers domain event
            await _repository.UpdateAsync(todo);
            
            // Dispatch the domain events
            await _dispatcher.DispatchAsync(todo.DomainEvents);
            todo.ClearDomainEvents(); // Clear after dispatching
        }
        public async Task<List<TodoDto>> GetAllTodosAsync(string? status = null, string? priority = null, DateTime? startDate = null, DateTime? endDate = null, string? orderBy = "Title" , string? sortOrder = "asc")
        {
            try
            {
                // Convert sortBy string to an expression
                var orderByExpression = GetOrderByExpression(orderBy);
                var todos = await _repository.GetAllAsync(status, priority, startDate, endDate, orderByExpression, sortOrder);
                return _mapper.Map<List<TodoDto>>(todos);
            }
            catch (Exception ex)
            {
                // Log exception and throw a service-specific exception
                throw new ServiceException("An error occurred while fetching the todos.", ex);
            }
        }

        public async Task<TodoDto?> GetTodoByIdAsync(Guid id)
        {
            try
            {
                var todo = await _repository.GetByIdAsync(id);
                if (todo == null)
                {
                    throw new NotFoundException("Todo not found.");
                }
                return _mapper.Map<TodoDto>(todo);
            }
            catch (NotFoundException)
            {
                throw; // Re-throw the NotFoundException
            }
            catch (Exception ex)
            {
                // Log and throw service-specific exception
                throw new ServiceException("An error occurred while fetching the todo by ID.", ex);
            }
        }

        public async Task<TodoDto> CreateTodoAsync(TodoDto todoDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(todoDto.Title))
                {
                    throw new ValidationException("Title is required.");
                }

                if (todoDto.DueDate.HasValue && todoDto.DueDate.Value <= DateTime.UtcNow)
                {
                    throw new ValidationException("Due date must be in the future.");
                }

                var todo = _mapper.Map<Todo>(todoDto);
                await _repository.AddAsync(todo);
                return _mapper.Map<TodoDto>(todo);
            }
            catch (ValidationException)
            {
                throw; // Re-throw the ValidationException
            }
            catch (Exception ex)
            {
                // Log and throw service-specific exception
                throw new ServiceException("An error occurred while creating the todo.", ex);
            }
        }

        public async Task UpdateTodoAsync(Guid id, TodoDto todoDto)
        {
            try
            {
                var todo = await _repository.GetByIdAsync(id);
                if (todo == null)
                {
                    throw new NotFoundException("Todo not found.");
                }

                if (string.IsNullOrWhiteSpace(todoDto.Title))
                {
                    throw new ValidationException("Title is required.");
                }

                if (todoDto.DueDate.HasValue && todoDto.DueDate.Value <= DateTime.UtcNow)
                {
                    throw new ValidationException("Due date must be in the future.");
                }

                _mapper.Map(todoDto, todo);
                await _repository.UpdateAsync(todo);
            }
            catch (NotFoundException)
            {
                throw; // Re-throw the NotFoundException
            }
            catch (ValidationException)
            {
                throw; // Re-throw the ValidationException
            }
            catch (Exception ex)
            {
                // Log and throw service-specific exception
                throw new ServiceException("An error occurred while updating the todo.", ex);
            }
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            try
            {
                var todo = await _repository.GetByIdAsync(id);
                if (todo == null)
                {
                    throw new NotFoundException("Todo not found.");
                }

                await _repository.DeleteAsync(id);
            }
            catch (NotFoundException)
            {
                throw; // Re-throw the NotFoundException
            }
            catch (Exception ex)
            {
                // Log and throw service-specific exception
                throw new ServiceException("An error occurred while deleting the todo.", ex);
            }
        }
        // A method to convert string to an expression
        private Expression<Func<Todo, object>> GetOrderByExpression(string? sortBy)
        {
            return sortBy?.ToLower() switch
            {
                "title" => t => t.Title,
                "createddate" => t => t.CreatedDate,
                "duedate" => t => t.DueDate,
                "priority" => t => t.Priority,
                "status" => t => t.Status,
                _ => throw new ValidationException($"Invalid sort property: {sortBy}")
            };
        }
    }
}
