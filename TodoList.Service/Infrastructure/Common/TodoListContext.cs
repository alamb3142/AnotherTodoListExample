using Microsoft.EntityFrameworkCore;
using Domain.Todos;
using Domain.Common;

#nullable disable
namespace Infrastructure.Common;

public class TodoListContext : DbContext, IUnitOfWork
{
	public DbSet<Todo> Todos { get; set; }

	public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}
#nullable enable
