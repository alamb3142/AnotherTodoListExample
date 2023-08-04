using Application.Todos;
using Domain.Todos;
using Infrastructure.Common;
using Infrastructure.Todos;
using Application;
using Api.Todos;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var config = builder.Configuration;
		builder.Services.Configure<DbSettings>(config);
		builder.Services.AddDbContext<TodoListContext>();

		builder.Services.AddScoped<ITodoRepository, TodoRepository>();
		builder.Services.AddScoped<ITodoReadRepository, TodoReadRepository>();

		builder.Services.AddGrpc();

		builder.Services.AddCors(o => o.AddPolicy("AllowAll", policy =>
		{
			policy.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
		}));

		builder.Services.AddApplication();

		var app = builder.Build();

		app.UseGrpcWeb(new GrpcWebOptions()
		{
			DefaultEnabled = true
		});
		app.UseCors("AllowAll");

		app.MapGrpcService<TodoService>().RequireCors("AllowAll");
		app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

		app.Run();
	}
}
