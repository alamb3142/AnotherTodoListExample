using System.Net;
using Application.Todos.CreateTodo;
using Application.Todos.GetAllTodos;
using Application.Todos.GetFilteredTodos;
using Application.Todos.DeleteTodo;
using FluentResults;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Application.Todos.RenameTodo;
using Application.Todos.CompleteTodo;

namespace Api.Http.Controllers;

[Controller]
[Route("Todos")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Rename")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> RenameTodo(
        RenameTodoCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(command, cancellationToken);
        return this.FromResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(List<IError>))]
    public async Task<ActionResult<int>> CreateTodo(
        CreateTodoCommand command,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(command, cancellationToken);
        return this.FromResult(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAllTodosQueryResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetAllTodosQueryResponse>> GetAll(
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(new GetAllTodosQuery());
        return Ok(response);
    }

    [HttpGet]
    [Route("filtered")]
    [ProducesResponseType(typeof(GetFilteredTodosQueryResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetFiltered(
        GetFilteredTodosQuery query,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    [Route("complete")]
    public async Task<ActionResult> Complete(
        CompleteTodoCommand command,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(command, cancellationToken);
        return this.FromResult(response);
    }
}
