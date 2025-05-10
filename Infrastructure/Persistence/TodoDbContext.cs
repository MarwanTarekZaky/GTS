using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Infrastructure.Persistence;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {}

    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureTodoEntity(modelBuilder);
    }

    private void ConfigureTodoEntity(ModelBuilder modelBuilder)
    {
        var todoEntity = modelBuilder.Entity<Todo>();

        // Title Configuration
        todoEntity.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        // Description Configuration (Optional)
        todoEntity.Property(t => t.Description)
            .HasMaxLength(500);

        // Status Configuration (Enum)
        todoEntity.Property(t => t.Status)
            .HasDefaultValue(TodoStatus.Pending)
            .HasConversion<string>(); // Store as string in the database for better readability

        // Priority Configuration (Enum)
        todoEntity.Property(t => t.Priority)
            .HasDefaultValue(TodoPriority.Medium)
            .HasConversion<string>(); // Store as string in the database for better readability

        // Date Configuration
        todoEntity.Property(t => t.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()"); // SQL Server-specific (use UtcNow in app for cross-db)

        todoEntity.Property(t => t.LastModifiedDate)
            .HasDefaultValueSql("GETUTCDATE()"); // Set default value to current UTC date
    }
}