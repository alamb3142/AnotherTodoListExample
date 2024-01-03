using Application.Users.Common;
using FluentResults;
using Mediator;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Users.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Result<string>>
{
    private readonly IJwtTokenService _tokenService;

    public RefreshTokenCommandHandler(IJwtTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public ValueTask<Result<string>> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken
    )
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("AStupidStupidStupidStuipdStuidasldkfjsalkfjsadfSecret")
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var handler = new JwtSecurityTokenHandler();

        var principal = handler.ValidateToken(
            command.Token,
            new TokenValidationParameters() { IssuerSigningKey = securityKey },
            out var validatedToken
        );
        var tokenHasId = Int32.TryParse(
            principal.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value,
            out var userId
        );

        if (validatedToken.ValidTo < DateTime.UtcNow.AddHours(-48) && tokenHasId)
        {
            var newToken = _tokenService.GenerateToken(userId);
            return ValueTask.FromResult(Result.Ok(newToken));
        }

        return ValueTask.FromResult(Result.Fail<string>("Failed to generate new token"));
    }
}
