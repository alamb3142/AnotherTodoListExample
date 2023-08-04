using Mediator;

namespace Application.Todos.GetAllTodos;

public class GetAllTodosQueryHandler : IQueryHandler<GetAllTodosQuery, GetAllTodosQueryResponse>
{
	private readonly ITodoReadRepository _repository;

	public GetAllTodosQueryHandler(ITodoReadRepository repository)
	{
		_repository = repository;
	}

	public async ValueTask<GetAllTodosQueryResponse> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
	{
		var todos = await _repository.GetAllAsync(cancellationToken);

		return new GetAllTodosQueryResponse() { Todos = todos };
	}
}
