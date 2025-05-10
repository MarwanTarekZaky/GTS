namespace TodoManagementAPI.Domain.Events
{
    public class TodoCompletedEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public Todo CompletedTodo { get; }

        public TodoCompletedEvent(Todo completedTodo)
        {
            CompletedTodo = completedTodo;
        }
    }
}