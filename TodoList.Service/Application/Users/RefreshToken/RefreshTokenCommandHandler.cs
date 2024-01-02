using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FluentResults;
using Mediator;
using Microsoft.IdentityModel.Tokens;

namespace Application.Users.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Result<string>>
{
    public async ValueTask<Result<string>> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken
    )
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("AStupidStupidStupidStuipdStuidasldkfjsalkfjsadfSecret")
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var handler = new JwtSecurityTokenHandler();
        handler.ValidateToken(
            command.Token,
            new TokenValidationParameters() { IssuerSigningKey = securityKey },
            out var validatedToken
        );

        if (validatedToken.ValidTo < DateTime.UtcNow) { }

        await Task.CompletedTask;

        return command.Token;

    }
}
