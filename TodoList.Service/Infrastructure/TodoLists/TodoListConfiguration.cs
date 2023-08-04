using Domain.Common.ValueObjects;
using Domain.TodoLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.TodoLists;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
	public void Configure(EntityTypeBuilder<TodoList> builder)
	{
		builder.Ignore(t => t.DomainEvents);

		builder.HasKey(t => t.Id);

		builder.Ignore(t => t.TodoIds);

		builder.OwnsOne<Title>(
			todoList => todoList.Title,
			title =>
			{
				title.Property(z => z.Value).IsRequired();
			}
		);

	}
}
