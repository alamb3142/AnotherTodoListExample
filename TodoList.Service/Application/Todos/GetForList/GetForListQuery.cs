using Application.Todos.Common;
using Mediator;

namespace Application.Todos.GetForList;

public record GetForListQuery : IQuery<IEnumerable<TodoDto>>
{
    public required int TodoListId { get; init; }
}

public class GetForListQueryHandler : IQueryHandler<GetForListQuery, IEnumerable<TodoDto>>
{
    private readonly ITodoReadRepository _repository;

    public GetForListQueryHandler(ITodoReadRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<IEnumerable<TodoDto>> Handle(GetForListQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetForListAsync(query.TodoListId);
    }
}
