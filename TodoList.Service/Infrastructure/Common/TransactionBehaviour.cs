using Mediator;

namespace Infrastructure.Common
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IMessage
    {
        private readonly TodoListContext _context;

        public TransactionBehaviour(TodoListContext context)
        {
            _context = context;
        }

        public async ValueTask<TResponse> Handle(
            TRequest message,
            CancellationToken cancellationToken,
            MessageHandlerDelegate<TRequest, TResponse> next
        )
        {
            var response = await next(message, cancellationToken);
            if (NotCommand(message))
                return response;

            cancellationToken.ThrowIfCancellationRequested();
            await _context.SaveChangesAsync(cancellationToken);

            return response;
        }

		/// <summary>
		/// Hacky fix because `where TRequest : ICommand<TResponse>` didn't work
		/// </summary>
        private static bool NotCommand(TRequest message)
        {
            return !message.GetType().IsAssignableTo(typeof(ICommand<TResponse>))
                && !message.GetType().IsAssignableTo(typeof(ICommand));
        }
    }
}
