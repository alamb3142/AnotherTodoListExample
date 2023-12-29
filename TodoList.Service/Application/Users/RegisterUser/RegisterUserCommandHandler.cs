using Application.Users.Common;
using Domain.Users;
using Domain.Users.Errors;
using FluentResults;
using Mediator;

namespace Application.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result>
{
	private IUserRepository _repository;
	private IPasswordHasherService _hasherService;

	public RegisterUserCommandHandler(
		IUserRepository repository,
		IPasswordHasherService hasherService
	)
	{
		_repository = repository;
		_hasherService = hasherService;
	}

	public async ValueTask<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
	{
		var salt = _hasherService.CreateSalt();
		var hashedPassword = _hasherService.HashPassword(command.Password, salt);

		var user = await _repository.GetByUsernameAsync(command.Username, cancellationToken);
		if (user is not null)
			return Result.Fail(new UsernameTakenError());

		var newUser = User.Create(command.Username, hashedPassword, salt);
		if (newUser.IsFailed)
			return newUser.ToResult();

		_repository.Create(newUser.Value);

		return Result.Ok();
	}
}
