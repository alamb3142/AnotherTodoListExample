using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Users.Common;
using Domain.Common.Errors;
using Domain.Users;
using FluentResults;
using Mediator;
using Microsoft.IdentityModel.Tokens;

namespace Application.Users.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<string>>
{
	private readonly IPasswordHasherService _hasherService;
	private readonly IUserRepository _repository;

	public LoginCommandHandler(IPasswordHasherService hasherService, IUserRepository repository)
	{
		_hasherService = hasherService;
		_repository = repository;
	}

	public async ValueTask<Result<string>> Handle(
		LoginCommand command,
		CancellationToken cancellationToken
	)
	{
		var user = await _repository.GetByUsernameAsync(command.Username, cancellationToken);
		if (user is null)
		{
			return Result.Fail(new NotFoundError(0, nameof(User)));
		}

		var validPassword = _hasherService.VerifyPassword(command.Password, user);
		if (!validPassword)
		{
			return Result.Fail(new Error("TODO: create authentication error class"));
		}

		user.Login();
		_repository.Update(user);

		var token = GenerateToken(user.Id);
		return Result.Ok(token);
	}

	private string GenerateToken(int userId)
	{
		var securityKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes("AStupidStupidStupidStuipdStuidasldkfjsalkfjsadfSecret")
		);
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new[] { new Claim("user_id", userId.ToString()) };

		var token = new JwtSecurityToken(
			issuer: null,
			audience: null,
			claims: claims,
			expires: DateTime.UtcNow.AddHours(1),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
