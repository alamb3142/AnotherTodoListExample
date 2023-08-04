using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Todos;
using FluentResults;
using FluentValidation;
using Mediator;

namespace Application.Todos.CreateTodo;

public class CreateTodoCommand : ICommand<Result<int>>
{
    public required string Title { get; init; }
}

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, Result<int>>
{
    private readonly ITodoRepository repository;

    public CreateTodoCommandHandler(ITodoRepository todoRepository)
    {
        repository = todoRepository;
    }

    public async ValueTask<Result<int>> Handle(
        CreateTodoCommand command,
        CancellationToken cancellationToken
    )
    {
        Result<Title> titleOrError = Title.Create(command.Title);
        if (titleOrError.IsFailed)
            titleOrError.ToResult<int>();

        var todo = Todo.Create(titleOrError.Value);
        var id = await repository.CreateAsync(todo, cancellationToken);
        return id;
    }
}

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
    }
}
