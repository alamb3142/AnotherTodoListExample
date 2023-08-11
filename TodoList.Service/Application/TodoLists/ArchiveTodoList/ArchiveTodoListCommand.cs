using Mediator;
using Domain.TodoLists;
using FluentValidation;

namespace Application.TodoLists.ArchiveTodoList;

public class ArchiveTodoListCommand : ICommand
{
    public int TodoListId { get; init; }
}

public class ArchiveTodoListCommandHandler : ICommandHandler<ArchiveTodoListCommand>
{
    private readonly ITodoListRepository repository;

    public ArchiveTodoListCommandHandler(ITodoListRepository repository)
    {
        this.repository = repository;
    }

    public async ValueTask<Unit> Handle(
        ArchiveTodoListCommand command,
        CancellationToken cancellationToken
    )
    {
        var todoList = await repository.GetByIdAsync(command.TodoListId, cancellationToken);

        todoList.Archive();

        repository.Update(todoList);
        return Unit.Value;
    }
}

public class ArchiveTodoListCommandValidator : AbstractValidator<ArchiveTodoListCommand>
{
    public ArchiveTodoListCommandValidator()
    {
        RuleFor(x => x.TodoListId).NotEmpty();
    }
}
