using TodoManagementAPI.Domain.Entities.Enums;
using TodoManagementAPI.Domain.Events;

public class Todo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TodoStatus Status { get; set; } = TodoStatus.Pending;
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

    // Domain Events Collection
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    // Method to raise events
    private void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    // Method to clear events (to be used after dispatching)
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // Business Logic - Mark as Completed
    public void MarkAsCompleted()
    {
        if (Status == TodoStatus.Completed)
            return;

        Status = TodoStatus.Completed;
        LastModifiedDate = DateTime.UtcNow;

        // Raise the TodoCompletedEvent
        AddDomainEvent(new TodoCompletedEvent(this));
    }
}