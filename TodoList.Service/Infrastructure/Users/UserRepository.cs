using Domain.Users;
using FluentResults;

namespace Infrastructure.Users;

public class UserRepository : IUserRepository
{
    public void Create(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result<User>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

	public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public void Update(User user)
    {
        throw new NotImplementedException();
    }
}
