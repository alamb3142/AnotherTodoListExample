using MediatR;

namespace Domain.TodoLists.Events;

public class TodoListArchivedDomainEvent : INotification
{
    private readonly TodoList _todoList;

    public int Id => _todoList.Id;

    public TodoListArchivedDomainEvent(TodoList todoList)
    {
        _todoList = todoList;
    }
}
