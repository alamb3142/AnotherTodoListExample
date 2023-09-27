using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.TodoLists;
using Domain.Todos;
using FluentResults;
using FluentValidation;
using Mediator;

namespace Application.Todos.CreateTodo;

public class CreateTodoCommand : ICommand<Result>
{
	public required string Title { get; init; }
	public int? TodoListId { get; init; }
}

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Result>
{
	private readonly ITodoRepository _todoRepository;
	private readonly ITodoListRepository _todoListRepository;

	public CreateTodoCommandHandler(ITodoRepository todoRepository, ITodoListRepository todoListRepository)
	{
		_todoRepository = todoRepository;
		_todoListRepository = todoListRepository;
	}

	public async ValueTask<Result> Handle(
		CreateTodoCommand command,
		CancellationToken cancellationToken
	)
	{
		Result<Title> titleResult = Title.Create(command.Title);
		if (titleResult.IsFailed)
			return titleResult.ToResult();

		var todo = Todo.Create(titleResult.Value);
		_todoRepository.Create(todo);

		if (!command.TodoListId.HasValue)
			return Result.Ok();

		var todoListResult = await _todoListRepository.GetByIdAsync(command.TodoListId.Value, cancellationToken);
		if (todoListResult.IsFailed)
			return todoListResult.ToResult();

		var todoList = todoListResult.Value;
		todo.AddToList(todoList);

		return Result.Ok();
	}
}

