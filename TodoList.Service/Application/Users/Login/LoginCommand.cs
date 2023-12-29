using FluentResults;
using Mediator;

namespace Application.Users.Login;

public class LoginCommand : ICommand<Result<string>>
{
	public required string Username { get; init; }
	public required string Password { get; init; }
}
