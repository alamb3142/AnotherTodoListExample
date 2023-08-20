using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.TodoLists;
using FluentResults;

namespace Domain.Todos;

public class Todo : Entity, IAggregateRoot
{
    public int? TodoListId { get; protected set; }
    public Title Title { get; protected set; }
    public bool Completed { get; protected set; }
    public DateTime? DueUtc { get; protected set; }

    private Todo(int id, Title title, bool completed, int? todoListId = null, DateTime? dueUtc = null) : this()
    {
        Id = id;
        Title = title;
        Completed = completed;
        TodoListId = todoListId;
        DueUtc = dueUtc;
    }

    public static Todo Create(Title title)
    {
        return new Todo(default, title, false);
    }

    public static Result<Todo> Create(string title)
    {
        var titleResult = Title.Create(title);
        if (titleResult.IsFailed)
            return Result.Fail<Todo>(titleResult.Errors);

        return Result.Ok(new Todo(default, titleResult.Value, false));
    }


    public Result Rename(string title)
    {
        var titleResult = Title.Create(title);
        if (titleResult.IsFailed)
            return Result.Fail(titleResult.Errors);

		Title = titleResult.Value;
        return Result.Ok();
    }

    public void Complete()
    {
        Completed = true;
    }

    public void AddToList(TodoList todoList)
    {
        // Todo domain event ?
        TodoListId = todoList.Id;
    }

    public void RemoveFromList()
    {
        TodoListId = null;
    }

#nullable disable
    private Todo() { }
#nullable enable
}
