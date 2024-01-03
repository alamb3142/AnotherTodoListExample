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
    private readonly IJwtTokenService _tokenService;
    private readonly IUserRepository _repository;

    public LoginCommandHandler(
        IPasswordHasherService hasherService,
        IJwtTokenService tokenService,
        IUserRepository repository
    )
    {
        _hasherService = hasherService;
        _repository = repository;
        _tokenService = tokenService;
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

		var token = _tokenService.GenerateToken(user.Id);
        return Result.Ok(token);
    }
}
