using Domain.Common.Errors;
using Domain.Users;
using FluentResults;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users;

public class UserRepository : IUserRepository
{
	private TodoListContext _context;

	public UserRepository(TodoListContext context)
	{
		_context = context;
	}

	public void Create(User user)
	{
		_context.Users.Add(user);
	}

	public async Task<Result<User>> GetByIdAsync(int id, CancellationToken cancellationToken)
	{
		var user = await _context.Users.FindAsync(id, cancellationToken);
		if (user is null)
			return Result.Fail(new NotFoundError(id, nameof(User)));

		return Result.Ok(user);
	}

	public async Task<User?> GetByUsernameAsync(
		string username,
		CancellationToken cancellationToken
	)
	{
		return await _context.Users.FirstOrDefaultAsync(
			u => u.Username == username,
			cancellationToken
		);
	}

	public void Update(User user)
	{
		_context.Users.Update(user);
	}
}
