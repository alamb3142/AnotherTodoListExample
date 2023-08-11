using System.Net;
using Application.Todos.CreateTodo;
using Application.Todos.GetAllTodos;
using Application.Todos.GetFilteredTodos;
using Application.Todos.DeleteTodo;
using Domain.Common.Errors;
using FluentResults;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Application.TodoLists.RenameTodo;

namespace Api.Http.Todos;

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
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Accepted();
        }

        if (result.HasError<NotFoundError>())
        {
            var error = result.Errors.First(x => x.GetType() == typeof(NotFoundError));
            return NotFound(error.Message);
        }

        return Problem(statusCode: (int)HttpStatusCode.InternalServerError);
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

        if (response.IsFailed)
            return BadRequest(response.Errors);

        return Ok(response.Value);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTodo(
        DeleteTodoCommand command,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(command, cancellationToken);
        return Accepted();
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAllTodosQueryResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetAllTodosQueryResponse>> GetAll(
        GetAllTodosQuery query,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(query);
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
}
