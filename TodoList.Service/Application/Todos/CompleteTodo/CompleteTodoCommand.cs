using Domain.Todos;
using FluentResults;
using Mediator;

namespace Application.Todos.CompleteTodo;

public class CompleteTodoCommand : ICommand<Result>
{
	public required int Id { get; init; }
}

public class CompleteTodoCommandHandler : ICommandHandler<CompleteTodoCommand, Result>
{
	private readonly ITodoRepository repository;

	public CompleteTodoCommandHandler(ITodoRepository repository)
	{
		this.repository = repository;
	}

	public async ValueTask<Result> Handle(
		CompleteTodoCommand command,
		CancellationToken cancellationToken
	)
	{
		var todoResult = await repository.GetByIdAsync(command.Id, cancellationToken);
		if (todoResult.IsFailed)
			return todoResult.ToResult();

		var todo = todoResult.Value;

		todo.Complete();

		repository.Update(todo);

		return Result.Ok();
	}
}
