using Application.Todos.CreateTodo;
using Application.Todos.DeleteTodo;
using Application.Todos.GetAllTodos;
using Application.Todos.GetFilteredTodos;
using Application.Todos.UpdateTodo;
using Grpc.Core;
using Grpc.Todos;
using Mediator;

namespace Api.Todos
{
    public class TodoService : Todo.TodoBase
    {
        private readonly IMediator _mediator;

        public TodoService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CreateTodoResponse> Create(
            CreateTodoRequest request,
            ServerCallContext context
        )
        {
            var command = new CreateTodoCommand() { Title = request.Title };

            var id = await _mediator.Send(command);

            return new CreateTodoResponse { Id = id.ValueOrDefault };
        }

        public override async Task<GetAllTodosResponse> GetAll(
            GetAllTodosRequest request,
            ServerCallContext context
        )
        {
            var todos = (await _mediator.Send(new GetAllTodosQuery())).Todos;

            var response = new GetAllTodosResponse();
            response.Todos.AddRange(
                todos
                    .Select(
                        t =>
                            new TodoDto()
                            {
                                Id = t.Id,
                                Title = t.Title,
                                Completed = t.Completed
                            }
                    )
                    .ToList()
            );

            return response;
        }

        public override async Task<GetFilteredTodosResponse> GetFiltered(
            GetFilteredTodosRequest request,
            ServerCallContext context
        )
        {
            var query = new GetFilteredTodosQuery()
            {
                SearchTerm = request.SearchTerm,
                Offset = request.Offset,
                FetchNum = request.FetchNum
            };
            var todos = (await _mediator.Send(query, context.CancellationToken)).Todos;

            var response = new GetFilteredTodosResponse();

            response.Todos.AddRange(
                todos.Select(
                    t =>
                        new TodoDto
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Completed = t.Completed
                        }
                )
            );
            return response;
        }

        public override async Task<UpdateTodoResponse> Update(
            UpdateTodoRequest request,
            ServerCallContext context
        )
        {
            var command = new UpdateTodoCommand()
            {
                Id = request.Id,
                Title = request.HasTitle ? request.Title : null,
                Completed = request.HasCompleted ? request.Completed : null
            };
            await _mediator.Send(command, context.CancellationToken);

            return new UpdateTodoResponse();
        }

        public override async Task<DeleteTodosResponse> Delete(
            DeleteTodosRequest request,
            ServerCallContext context
        )
        {
            var command = new DeleteTodoCommand() { Id = request.Id };
            await _mediator.Send(command, context.CancellationToken);
            return new DeleteTodosResponse();
        }
    }
}
