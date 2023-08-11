using Domain.Common.ValueObjects;
using Domain.TodoLists;
using FluentResults;
using Mediator;

namespace Application.TodoLists.CreateTodoList;

public class CreateTodoListCommand : ICommand<Result<int>>
{
    public required string Name { get; init; }
}

public class CreateTodoListCommandHandler : ICommandHandler<CreateTodoListCommand, Result<int>>
{
    private readonly ITodoListRepository _repository;

    public CreateTodoListCommandHandler(ITodoListRepository repository)
    {
        _repository = repository;
    }

    public ValueTask<Result<int>> Handle(
        CreateTodoListCommand command,
        CancellationToken cancellationToken
    )
    {
        var titleResult = Title.Create(command.Name);

        if (titleResult.IsFailed)
        {
            var badResult = Result.Fail<int>(titleResult.Errors);
            return ValueTask.FromResult(badResult);
        }
        var todoList = TodoList.Create(titleResult.Value);

        _repository.Create(todoList);
        var okRestult = Result.Ok(todoList.Id);

        return ValueTask.FromResult(okRestult);
    }
}
