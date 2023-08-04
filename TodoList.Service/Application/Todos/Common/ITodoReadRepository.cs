using Application.Todos.Common;

namespace Application.Todos;

public interface ITodoReadRepository
{
	public Task<List<TodoDto>> GetAllAsync(CancellationToken cancellationToken = default);
	public Task<List<TodoDto>> GetFilteredAsync(string? searchTerm, int offset, int fetchNum, CancellationToken cancellationToken = default);
}
