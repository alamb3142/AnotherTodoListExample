using Domain.TodoLists;
using FluentResults;
using Mediator;

namespace Application.TodoLists.Rename;

public class RenameTodoListCommand : ICommand<Result>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}

public class RenameTodoListCommandHandler : ICommandHandler<RenameTodoListCommand, Result>
{
    private readonly ITodoListRepository _repository;

    public RenameTodoListCommandHandler(ITodoListRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(RenameTodoListCommand command, CancellationToken cancellationToken)
    {
        var todoListResult = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (todoListResult.IsFailed)
            return Result.Fail(todoListResult.Errors);

        var todoList = todoListResult.Value;
        var renameResult = todoList.Rename(command.Name);

        if (renameResult.IsFailed)
            return Result.Fail(renameResult.Errors);

        return Result.Ok();
    }
}
