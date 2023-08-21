using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Todos;
using FluentResults;
using FluentValidation;
using Mediator;

namespace Application.Todos.CreateTodo;

public class CreateTodoCommand : ICommand<Result>
{
	public required string Title { get; init; }
}

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Result>
{
	private readonly ITodoRepository repository;

	public CreateTodoCommandHandler(ITodoRepository todoRepository)
	{
		repository = todoRepository;
	}

	public ValueTask<Result> Handle(
		CreateTodoCommand command,
		CancellationToken cancellationToken
	)
	{
		Result<Title> titleResult = Title.Create(command.Title);
		if (titleResult.IsFailed)
			return ValueTask.FromResult(titleResult.ToResult());

		var todo = Todo.Create(titleResult.Value);
		repository.Create(todo);

		var result = Result.Ok();
		return new ValueTask<Result>(result);
	}
}

