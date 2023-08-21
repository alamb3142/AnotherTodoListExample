namespace Application.TodoLists.Common;

public interface ITodoListReadRepository
{
    public Task<IEnumerable<TodoListSummaryDto>> GetTodoListSummariesAsync(CancellationToken cancellation);
}
