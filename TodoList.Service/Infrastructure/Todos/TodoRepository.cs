using Domain.Common;
using Domain.Common.Errors;
using Domain.Todos;
using FluentResults;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Todos;

public class TodoRepository : ITodoRepository, IDisposable
{
    private readonly TodoListContext _context;

    IUnitOfWork IRepository<Todo>.UnitOfWork => _context;

    public TodoRepository(TodoListContext context)
    {
        _context = context;
    }

    public void Create(Todo todo)
    {
        _context.Todos.Add(todo);
    }

    public Task<List<Todo>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _context.Todos.ToListAsync(cancellationToken);
    }

    public async Task<Result> DeleteAsync(int todoId, CancellationToken cancellationToken)
    {
        var todo = await _context.Todos.FindAsync(todoId, cancellationToken);

        if (todo is null)
            return Result.Fail(new NotFoundError(todoId, nameof(Todo)));

        _context.Remove(todo);

        return Result.Ok();
    }

    public async Task<Result<Todo>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (todo is null)
            return Result.Fail(new NotFoundError(id, nameof(Todo)));

        return Result.Ok(todo);
    }

	public void Update(Todo todo)
	{
		_context.Todos.Update(todo);
	}

	public void Dispose()
	{
		_context.SaveChanges();
	}
}
