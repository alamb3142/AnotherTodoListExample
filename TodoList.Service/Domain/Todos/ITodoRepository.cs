using Domain.Common;
using FluentResults;

namespace Domain.Todos;

public interface ITodoRepository : IRepository<Todo>
{
	public Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken);

	public Task<Result<Todo>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	public Task<int> CreateAsync(Todo todo, CancellationToken cancellationToken);

	public Task<Result> DeleteAsync(int todoId, CancellationToken cancellationToken);
}
