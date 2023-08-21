using Domain.Todos;
using FluentResults;
using Mediator;

namespace Application.Todos.RenameTodo;

public class RenameTodoCommand : ICommand<Result>
{
	public required int Id { get; init; }
	public required string Title { get; init; }
}

public class RenameTodoCommandHandler : ICommandHandler<RenameTodoCommand, Result>
{
	private readonly ITodoRepository repository;

	public RenameTodoCommandHandler(ITodoRepository repository)
	{
		this.repository = repository;
	}

	public async ValueTask<Result> Handle(
		RenameTodoCommand command,
		CancellationToken cancellationToken
	)
	{
		var todoResult = await repository.GetByIdAsync(command.Id, cancellationToken);
		if (todoResult.IsFailed)
			return todoResult.ToResult();

		var todo = todoResult.Value;

		var renameResult = todo.Rename(command.Title);
		if (renameResult.IsFailed)
			return renameResult;

		repository.Update(todo);

		return Result.Ok();
	}
}
