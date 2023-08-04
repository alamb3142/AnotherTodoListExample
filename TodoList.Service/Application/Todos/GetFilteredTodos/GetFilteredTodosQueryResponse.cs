using Application.Todos.Common;

namespace Application.Todos.GetFilteredTodos;

public class GetFilteredTodosQueryResponse
{
    public IEnumerable<TodoDto> Todos { get; init; } = new List<TodoDto>();
}
