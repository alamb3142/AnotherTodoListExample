using Domain.Todos;
using Domain.Common.Errors;
using FluentResults;
using Mediator;

namespace Application.TodoLists.RenameTodo;

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
        {
            return Result.Fail(new NotFoundError(command.Id, nameof(Todo)));
        }

        var todo = todoResult.Value;

        var renameResult = todo.Rename(command.Title);

        return Result.Ok();
    }
}
