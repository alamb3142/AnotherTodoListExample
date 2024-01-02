using FluentResults;
using Mediator;

namespace Application.Users.RefreshToken;

public class RefreshTokenCommand : ICommand<Result<string>>
{
	public required string Token { get; init; }
}
