using FluentResults;

namespace Domain.Users;

public interface IUserRepository
{
	public Task<Result<User>> GetByIdAsync(int id, CancellationToken cancellationToken);
	public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
	public void Create(User user);
	public void Update(User user);
}
