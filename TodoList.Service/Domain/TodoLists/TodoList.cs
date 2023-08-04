using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.TodoLists.Events;
using FluentResults;

namespace Domain.TodoLists;

public class TodoList : Entity, IAggregateRoot
{
	public Title Title { get; protected set; }
	public bool Archived { get; protected set; } = false;
	private readonly List<int> _todoIds;
	public IReadOnlyCollection<int> TodoIds => _todoIds.AsReadOnly();

	protected TodoList(int id, Title title, List<int> todoIds)
	{
		Id = id;
		Title = title;
		_todoIds = todoIds;
	}

	public static TodoList Create(Title title)
	{
		return new TodoList(default, title, new List<int>());
	}

	public Result Rename(string title)
	{
		var newTitle = Title.Create(title);
		if (newTitle.IsFailed)
			return Result.Fail(newTitle.Errors);

		Title = newTitle.Value;
		return Result.Ok();
	}

	public void Archive()
	{
		Archived = true;
		AddDomainEvent(new TodoListArchivedDomainEvent(this));
	}

#nullable disable
	// to please the EF gods
	protected TodoList() { }
#nullable enable
}
