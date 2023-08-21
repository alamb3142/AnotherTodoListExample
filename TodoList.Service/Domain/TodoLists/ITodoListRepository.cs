using Domain.Common;
using FluentResults;

namespace Domain.TodoLists;

public interface ITodoListRepository : IRepository<TodoList>
{
    public Task<Result<TodoList>> GetByIdAsync(int todoListId, CancellationToken cancellationToken);
    public void Update(TodoList todoList);
    public void Create(TodoList todoList);
}
