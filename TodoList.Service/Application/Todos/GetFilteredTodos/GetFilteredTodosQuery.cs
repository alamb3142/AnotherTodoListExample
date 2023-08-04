using Mediator;

namespace Application.Todos.GetFilteredTodos;

public class GetFilteredTodosQuery : IQuery<GetFilteredTodosQueryResponse>
{
    public string? SearchTerm { get; set; }
    public int Offset { get; set; } = 0;
    public int FetchNum { get; set; } = 50;
}
