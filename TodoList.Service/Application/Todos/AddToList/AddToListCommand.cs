using Domain.TodoLists;
using Domain.Todos;
using FluentResults;
using Mediator;

namespace Application.Todos.AddToList;

public record AddToListCommand : ICommand<Result>
{
    public required int TodoListId { get; init; }
    public required int TodoId { get; init; }
}

public class AddToListCommandHandler : ICommandHandler<AddToListCommand, Result>
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoListRepository _todoListRepository;

    public AddToListCommandHandler(ITodoRepository repository, ITodoListRepository todoListRepository)
    {
        _todoRepository = repository;
        _todoListRepository = todoListRepository;
    }

    public async ValueTask<Result> Handle(AddToListCommand command, CancellationToken cancellationToken)
    {
        var todoResult = await _todoRepository.GetByIdAsync(command.TodoId, cancellationToken);
        if (todoResult.IsFailed)
            return todoResult.ToResult();
        var todo = todoResult.Value;

        var todoListResult = await _todoListRepository.GetByIdAsync(command.TodoListId, cancellationToken);
        if (todoListResult.IsFailed)
            return todoListResult.ToResult();
        var todoList = todoListResult.Value;

        todo.AddToList(todoList);

        _todoRepository.Update(todo);

        return Result.Ok();
    }
}
