using Application;
using Infrastructure.Common;
using Infrastructure;
using Mediator;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var dbSettings = new DbSettings()
		{
			DatabaseName = "TodoList",
			Server = "localhost"
		};

		builder.Services.AddInfrastructure(dbSettings);
		builder.Services.AddApplication();
		builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

		builder.Services.AddControllers();
		// builder.Services.AddEndpointsApiExplorer();
		// builder.Services.AddSwaggerGen();
		builder.Services.AddOpenApiDocument();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			// app.UseSwagger();
			// app.UseSwaggerUI();

			app.UseOpenApi(options =>
			{
				options.DocumentName = "Goodbye world";
			});
			app.UseSwaggerUi3(options =>
			{
				options.DefaultModelsExpandDepth = 4;
				options.DocExpansion = "list";
				options.DocumentTitle = "Todos";
			});

			app.UseDeveloperExceptionPage();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
