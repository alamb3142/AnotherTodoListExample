using System.Net;
using Application.TodoLists.Common;
using Application.TodoLists.GetAllTodoLists;
using Application.TodoLists.CreateTodoList;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Application.TodoLists.ArchiveTodoList;
using Application.TodoLists.Rename;

namespace Api.Http.Controllers;

[Controller]
[Route("TodoLists")]
public class TodoListController : ControllerBase
{
	private readonly IMediator _mediator;

	public TodoListController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	[ProducesResponseType(typeof(IEnumerable<TodoListSummaryDto>), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<IEnumerable<TodoListSummaryDto>>> GetAll(
		GetAllTodoListsQuery query,
		CancellationToken cancellationToken
	)
	{
		IEnumerable<TodoListSummaryDto> result = await _mediator.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpPost]
	[ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<ActionResult<int>> Create(
		CreateTodoListCommand command,
		CancellationToken cancellationToken
	)
	{
		Result<int> result = await _mediator.Send(command, cancellationToken);
		return this.FromResult(result);
	}

	[HttpPost]
	[Route("Archive")]
	[ProducesResponseType((int)HttpStatusCode.Accepted)]
	public async Task<ActionResult> Archive(
		ArchiveTodoListCommand command,
		CancellationToken cancellationToken
	)
	{
		Result result = await _mediator.Send(command, cancellationToken);
		return this.FromResult(result);
	}

	[HttpPost]
	[Route("Rename")]
	[ProducesResponseType((int)HttpStatusCode.Accepted)]
	public async Task<ActionResult> Rename(
		RenameTodoListCommand command,
		CancellationToken cancellationToken
	)
	{
		Result result = await _mediator.Send(command, cancellationToken);
		return this.FromResult(result);
	}
}
