using Domain.Todos;
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

    public async ValueTask<Result> Handle(RenameTodoCommand command, CancellationToken cancellationToken)
    {
        var todoResult = await repository.GetByIdAsync(command.Id, cancellationToken);

        if (todoResult.IsFailed)
        {
            // TODO move this into the domain & create the NotFoundError in the repo method
            return Result.Fail(new NotFoundError(command.Id, nameof(Todo)));
        }

        var todo = todoResult.Value;

        var renameResult = todo.Rename(command.Title);

        return Result.Ok();
    }
}

public class NotFoundError : Error
{
    public NotFoundError(int id, string entityName) : base($"Couldn't find {entityName} with ID {id}") { }
}
