using System.Net;
using Application.Todos.CreateTodo;
using Application.Todos.GetAllTodos;
using Application.Todos.GetFilteredTodos;
using Application.Todos.DeleteTodo;
using Application.Todos.UpdateTodo;
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

	[HttpPost]
	public async Task<ActionResult> RenameTodo(RenameTodoCommand command, CancellationToken cancellationToken)
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
	public async Task<ActionResult<int>> CreateTodo(CreateTodoCommand command, CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(command, cancellationToken);

		if (response.IsFailed)
			return BadRequest(response.Errors);

		return Ok(response.Value);
	}

	[HttpPut]
	public async Task<ActionResult> UpdateTodo(UpdateTodoCommand command, CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(command, cancellationToken);
		return Accepted();
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteTodo(DeleteTodoCommand command, CancellationToken cancellationToken)
	{
		await _mediator.Send(command, cancellationToken);
		return Accepted();
	}

	[Route("todos")]
	[ProducesResponseType(typeof(GetAllTodosQueryResponse), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<GetAllTodosQueryResponse>> GetAll(GetAllTodosQuery query, CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(query);
		return Ok(response);
	}

	[HttpGet]
	[ProducesResponseType(typeof(GetFilteredTodosQueryResponse), (int)HttpStatusCode.OK)]
	public async Task<ActionResult> GetFiltered(GetFilteredTodosQuery query, CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(query);
		return Ok(response);
	}
}
