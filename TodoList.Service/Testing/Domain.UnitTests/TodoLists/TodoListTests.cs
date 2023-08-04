using Domain.Common.ValueObjects;
using Domain.TodoLists;
using Domain.TodoLists.Events;
using FluentAssertions;

namespace Testing.Domain.TodoLists;

[TestFixture]
public class TodoListTests
{
    private readonly Title validTitle = Title.Create("My List").Value;

    [Test]
    public void Create_WithValidTitle_ReturnsTodoList()
    {
        var todoList = TodoList.Create(validTitle);

        todoList.Should().NotBeNull();

        todoList.Title.Should().Be(validTitle);
        todoList.Archived.Should().BeFalse();
    }

    [Test]
    public void Rename_WithValidTitle_UpdatesTitle()
    {
        var todoList = TodoList.Create(validTitle);

        var newTitle = Title.Create("Daily Tasks").Value;
        todoList.Rename(newTitle);

        todoList.Title.Should().Be(newTitle);
    }

    [Test]
    public void Rename_WithValidString_ReturnsOkResult()
    {
        var todoList = TodoList.Create(validTitle);

        string newTitle = "Daily Tasks";
        var result = todoList.Rename(newTitle);

        todoList.Title.Value.Should().Be(newTitle);
        result.IsSuccess.Should().Be(true);
    }

    [Test]
    public void Rename_WithInvalidString_ReturnsFailedResult()
    {
        var todoList = TodoList.Create(validTitle);

        string newTitle = "";
        var result = todoList.Rename(newTitle);

        result.IsFailed.Should().BeTrue();
        todoList.Title.Should().Be(validTitle);
    }

    [Test]
    public void Archive_SetsArchivedTrue()
    {
        var todoList = TodoList.Create(validTitle);

        todoList.Archive();

        todoList.Archived.Should().BeTrue();
    }

    [Test]
    public void Archive_RaisesTodoListArchivedDomainEvent()
    {
        var todoList = TodoList.Create(validTitle);

        todoList.Archive();

        var archiveEvents = todoList.DomainEvents
            .OfType<TodoListArchivedDomainEvent>()
            .ToList();
        archiveEvents.Count.Should().Be(1);
        archiveEvents.FirstOrDefault().Should().NotBeNull();
    }
}
