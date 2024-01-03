using Application.Users.Login;
using Application.Users.RegisterUser;
using Application.Users.RefreshToken;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Http.Controllers;

[Controller]
[Route("Users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> RegisterUser(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(command, cancellationToken);
        return this.FromResult(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(command, cancellationToken);
        return this.FromResult(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<string>> RefreshToken(
        [FromBody] RefreshTokenCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(command, cancellationToken);
        return this.FromResult(result);
    }
}
