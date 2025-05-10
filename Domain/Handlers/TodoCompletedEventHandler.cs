namespace TodoManagementAPI.Domain.Events.Handlers
{
    public class TodoCompletedEventHandler : IDomainEventHandler<TodoCompletedEvent>
    {
        public Task HandleAsync(TodoCompletedEvent domainEvent)
        {
            // Handle the event (e.g., send notification, log, etc.)
            Console.WriteLine($"Todo Completed: {domainEvent.CompletedTodo.Title}");
            return Task.CompletedTask;
        }
    }
}