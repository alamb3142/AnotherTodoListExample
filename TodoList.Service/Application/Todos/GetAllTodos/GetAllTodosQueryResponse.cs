using Application.Todos.Common;

namespace Application.Todos.GetAllTodos;

public class GetAllTodosQueryResponse
{
    public IEnumerable<TodoDto> Todos { get; init; } = new List<TodoDto>();
}
