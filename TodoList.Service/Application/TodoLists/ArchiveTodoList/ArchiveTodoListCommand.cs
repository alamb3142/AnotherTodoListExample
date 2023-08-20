using Mediator;
using Domain.TodoLists;
using FluentValidation;
using FluentResults;

namespace Application.TodoLists.ArchiveTodoList;

public class ArchiveTodoListCommand : ICommand<Result>
{
    public int TodoListId { get; init; }
}

public class ArchiveTodoListCommandHandler : ICommandHandler<ArchiveTodoListCommand, Result>
{
    private readonly ITodoListRepository repository;

    public ArchiveTodoListCommandHandler(ITodoListRepository repository)
    {
        this.repository = repository;
    }

    public async ValueTask<Result> Handle(
        ArchiveTodoListCommand command,
        CancellationToken cancellationToken
    )
    {
        var todoListResult = await repository.GetByIdAsync(command.TodoListId, cancellationToken);
        if (todoListResult.IsFailed)
            return Result.Fail(todoListResult.Errors);

        var todoList = todoListResult.Value;

        todoList.Archive();

        repository.Update(todoList);
        return Result.Ok();
    }
}

public class ArchiveTodoListCommandValidator : AbstractValidator<ArchiveTodoListCommand>
{
    public ArchiveTodoListCommandValidator()
    {
        RuleFor(x => x.TodoListId).NotEmpty();
    }
}
