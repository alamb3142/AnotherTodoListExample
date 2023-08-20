using Domain.Common;
using Domain.Common.Errors;
using Domain.TodoLists;
using FluentResults;
using Infrastructure.Common;

namespace Infrastructure.TodoLists;

internal class TodoListRepository : ITodoListRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    private readonly TodoListContext _context;

    public TodoListRepository(TodoListContext context)
    {
        _context = context;
    }

    public void Create(TodoList todoList)
    {
        _context.TodoLists.Add(todoList);
    }

    public async Task<Result<TodoList>> GetByIdAsync(int todoListId, CancellationToken cancellationToken)
    {
        var todoList = await _context.TodoLists.FindAsync(todoListId);

        if (todoList is null)
            return Result.Fail(new NotFoundError(todoListId, nameof(TodoList)));

        return Result.Ok(todoList);
    }

    public void Update(TodoList todoList)
    {
        // TODO: check if this is necessary or if saving is just an automatic part of EF Core
        _context.TodoLists.Update(todoList);
    }
}
