using Domain.Todos;
using FluentValidation;
using Mediator;

namespace Application.Todos.DeleteTodo;

public class DeleteTodoCommand : ICommand
{
    public int Id { get; init; }
}

public class DeleteTodoCommandHandler : ICommandHandler<DeleteTodoCommand>
{
    private readonly ITodoRepository repository;

    public DeleteTodoCommandHandler(ITodoRepository todoRepository)
    {
        repository = todoRepository;
    }

    public async ValueTask<Unit> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(command.Id, cancellationToken);
        return new Unit();
    }
}

public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
