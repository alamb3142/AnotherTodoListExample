using Domain.Common.ValueObjects;
using Domain.TodoLists;
using Domain.Todos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Todos;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
	public void Configure(EntityTypeBuilder<Todo> builder)
	{
		builder.Ignore(t => t.DomainEvents);

		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id).IsRequired();

		builder.Property(t => t.Completed)
			.IsRequired()
			.HasDefaultValue(false);

		builder.OwnsOne<Title>(
			todo => todo.Title,
			title =>
			{
				title.Property(z => z.Value).IsRequired();
			}
		);

		builder.Property(t => t.TodoListId).IsRequired(false);

		builder.HasOne<TodoList>()
			.WithMany()
			.HasForeignKey(x => x.TodoListId)
			.IsRequired(false)
			.OnDelete(DeleteBehavior.SetNull);
	}
}
