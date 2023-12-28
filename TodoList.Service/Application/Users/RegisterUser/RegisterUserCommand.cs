using FluentResults;
using Mediator;

namespace Application.Users.RegisterUser;

public class RegisterUserCommand : ICommand<Result>
{
	public required string Username { get; init; }
	public required string Password { get; init; }
}
