using Approaches.Handlers;
using MediatR;
using StructureMap;
using System;
using System.Threading.Tasks;
using System.Threading;
using Approaches.Queries;
using Approaches.Responses;
using MediatR.Pipeline;

namespace Approaches
{
	class Program
	{
		static async Task MainAsync()
		{
			Console.WriteLine($"[{ThreadId()}] [{nameof(MainAsync)}] On MainAsync");

			var mediator = GetMediator();

			var query = new GetCustomerQuery(Guid.Empty);
			CustomerResponse customer = null;
			try
			{
				// Handled in ExceptionHandler
				customer = await mediator.Send(query);
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine($"[{ThreadId()}] [{nameof(MainAsync)}] An error occurred on send: {ex.Message}");
			}

			Console.WriteLine(customer is null
						? $"[{ThreadId()}] [{nameof(MainAsync)}] Customer not found"
						: $"[{ThreadId()}] [{nameof(MainAsync)}] Found customer '{customer?.FullName}'");

			try
			{
				// The default implementation of Publish loops through
				// the notification handlers and awaits each one.
				// This ensures each handler is run after one another.
				// --> not notified handlers will not be called after an exception occurred
				//     (see other publish strategies on project's wiki page)
				await mediator.Publish(query);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[{ThreadId()}] [{nameof(MainAsync)}] An error occurred on publish: {ex.Message}");
			}

			Console.ReadLine();
		}

		private static IMediator GetMediator()
		{
			var container = new Container(cfg =>
			{
				cfg.Scan(scanner =>
				{
					scanner.AssemblyContainingType<GetCustomerQueryHandler>();
					scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
					scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
					scanner.ConnectImplementationsToTypesClosing(typeof(IRequestExceptionHandler<,,>));
					scanner.ConnectImplementationsToTypesClosing(typeof(IRequestExceptionAction<,>));
				});


				cfg.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestExceptionProcessorBehavior<,>));

				cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
				cfg.For<IMediator>().Use<Mediator>();
			});

			return container.GetInstance<IMediator>();
		}

		static void Main(string[] args)
		{
			Console.WriteLine($"[{ThreadId()}] [{nameof(Main)}] On Main");
			MainAsync().GetAwaiter().GetResult();
		}

		private static int ThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}
	}
}
