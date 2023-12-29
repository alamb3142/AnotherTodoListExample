using Microsoft.EntityFrameworkCore;
using Domain.Todos;
using Domain.Common;
using Domain.TodoLists;
using Domain.Users;

#nullable disable
namespace Infrastructure.Common;

public class TodoListContext : DbContext, IUnitOfWork
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<User> Users { get; set; }

    public TodoListContext(DbContextOptions<TodoListContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
#nullable enable
