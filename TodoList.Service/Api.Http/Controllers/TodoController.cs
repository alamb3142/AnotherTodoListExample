using System.Net;
using Application.Todos.CreateTodo;
using Application.Todos.GetAllTodos;
using Application.Todos.GetFilteredTodos;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Application.Todos.RenameTodo;
using Application.Todos.CompleteTodo;
using Application.Todos.AddToList;
using Application.Todos.Common;
using Application.Todos.GetForList;

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
		Result result = await _mediator.Send(command, cancellationToken);
		return this.FromResult(result);
	}

	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesErrorResponseType(typeof(List<IError>))]
	public async Task<ActionResult<int>> CreateTodo(
		CreateTodoCommand command,
		CancellationToken cancellationToken
	)
	{
		Result response = await _mediator.Send(command, cancellationToken);
		return this.FromResult(response);
	}

	[HttpGet]
	[ProducesResponseType(typeof(GetAllTodosQueryResponse), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<GetAllTodosQueryResponse>> GetAll(
		CancellationToken cancellationToken
	)
	{
		GetAllTodosQueryResponse response = await _mediator.Send(new GetAllTodosQuery(), cancellationToken);
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
		GetFilteredTodosQueryResponse response = await _mediator.Send(query);
		return Ok(response);
	}

	[HttpPost]
	[Route("complete")]
	[ProducesResponseType((int)HttpStatusCode.Accepted)]
	public async Task<ActionResult> Complete(
		CompleteTodoCommand command,
		CancellationToken cancellationToken
	)
	{
		Result response = await _mediator.Send(command, cancellationToken);
		return this.FromResult(response);
	}

	[HttpPost]
	[Route("addToList")]
	[ProducesResponseType((int)HttpStatusCode.Accepted)]
	public async Task<ActionResult> AddToList(
		AddToListCommand command,
		CancellationToken cancellationToken
	)
	{
		Result response = await _mediator.Send(command, cancellationToken);
		return this.FromResult(response);
	}

	[HttpGet]
	[Route("forList")]
	[ProducesResponseType(typeof(IEnumerable<TodoDto>), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<IEnumerable<TodoDto>>> GetForList(
		GetForListQuery query,
		CancellationToken cancellationToken
	)
	{
		IEnumerable<TodoDto> response = await _mediator.Send(query, cancellationToken);
		return Ok(response);
	}
}
