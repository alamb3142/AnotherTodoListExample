using Domain.Common;
using FluentResults;

namespace Domain.Todos;

public interface ITodoRepository : IRepository<Todo>
{
    public Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<Todo>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<Result> DeleteAsync(int todoId, CancellationToken cancellationToken = default);
	public void Create(Todo todo);
    public void Update(Todo todo);
}
