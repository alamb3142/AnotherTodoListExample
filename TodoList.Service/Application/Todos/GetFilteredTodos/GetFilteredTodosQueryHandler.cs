using Mediator;

namespace Application.Todos.GetFilteredTodos;

public class GetFilteredTodosQueryHandler : IQueryHandler<GetFilteredTodosQuery, GetFilteredTodosQueryResponse>
{
	private readonly ITodoReadRepository _repository;

	public GetFilteredTodosQueryHandler(ITodoReadRepository repository)
	{
		_repository = repository;
	}

	public async ValueTask<GetFilteredTodosQueryResponse> Handle(GetFilteredTodosQuery query, CancellationToken cancellationToken)
	{
		var todos = await _repository.GetFilteredAsync(query.SearchTerm, query.Offset, query.FetchNum, cancellationToken);
		return new GetFilteredTodosQueryResponse() { Todos = todos };
	}
}
