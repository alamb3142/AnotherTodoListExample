using Domain.Common;

namespace Domain.TodoLists;

public interface ITodoListRepository : IRepository<TodoList>
{
    public Task<TodoList> GetByIdAsync(int todoListId, CancellationToken cancellationToken);
    public void Update(TodoList todoList);
    public void Create(TodoList todoList);
}
