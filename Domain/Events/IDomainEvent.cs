namespace TodoManagementAPI.Domain.Events;

// Domain/Events/IDomainEvent.cs
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
