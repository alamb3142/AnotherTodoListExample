using Domain.Common.ValueObjects;
using Domain.Todos;
using FluentValidation;
using Mediator;

namespace Application.Todos.UpdateTodo;

public class UpdateTodoCommand : ICommand
{
	public int Id { get; init; }
	public string? Title { get; init; }
	public bool? Completed { get; init; }
}

public class UpdateTodoCommandHandler : ICommandHandler<UpdateTodoCommand>
{
	private readonly ITodoRepository repository;

	public UpdateTodoCommandHandler(ITodoRepository todoRepository)
	{
		repository = todoRepository;
	}

	public async ValueTask<Unit> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
	{
		var todo = (await repository.GetByIdAsync(command.Id, cancellationToken)).Value;

		if (command.Title is not null)
		{
			var titleResult = Title.Create(command.Title);

			if (titleResult.IsFailed)
				throw new ApplicationException(titleResult.Errors.ToString());

			todo.UpdateTitle(titleResult.Value);
		}

		if (command.Completed.HasValue && command.Completed == true)
			todo.Complete();

		return new Unit();
	}
}

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
	public UpdateTodoCommandValidator()
	{
		RuleFor(x => x.Completed).NotNull().When(x => x.Title is null);
		RuleFor(x => x.Title).NotEmpty().When(x => !x.Completed.HasValue);
	}
}
