using Application.TodoLists.Common;
using Mediator;

namespace Application.TodoLists.GetAllTodoLists;

public class GetAllTodoListsQuery : IQuery<IEnumerable<TodoListSummaryDto>>
{
}

public class GetAllTodoListsQueryHandler : IQueryHandler<GetAllTodoListsQuery, IEnumerable<TodoListSummaryDto>>
{
    private readonly ITodoListReadRepository repository;

    public GetAllTodoListsQueryHandler(ITodoListReadRepository repository)
    {
        this.repository = repository;
    }

    public async ValueTask<IEnumerable<TodoListSummaryDto>> Handle(GetAllTodoListsQuery query, CancellationToken cancellationToken)
    {
        return await repository.GetTodoListSummariesAsync(cancellationToken);
    }
}
