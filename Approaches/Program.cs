using Approaches.Handlers;
using MediatR;
using StructureMap;
using System;
using System.Threading.Tasks;
using System.Threading;
using Approaches.Queries;
using Approaches.Responses;

namespace Approaches
{
	class Program
	{
		static async Task MainAsync()
		{

			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] On MainAsync");

			var mediator = GetMediator();

			var query = new GetCustomerQuery(Guid.Empty);
			CustomerResponse customer = null;
			try
			{
				customer = await mediator.Send(query);
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine($"An error occurred on send: {ex.Message}");
			}

			Console.WriteLine(customer is null
				? $"[{Thread.CurrentThread.ManagedThreadId}] Customer not found"
				: $"[{Thread.CurrentThread.ManagedThreadId}] Found customer '{customer?.FullName}'");

			try
			{
				// The default implementation of Publish loops through
				// the notification handlers and awaits each one.
				// This ensures each handler is run after one another.
				await mediator.Publish(query);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred on publish: {ex.Message}");
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
				});

				cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
				cfg.For<IMediator>().Use<Mediator>();
			});

			return container.GetInstance<IMediator>();
		}

		static void Main(string[] args)
		{
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] On Main");
			MainAsync().GetAwaiter().GetResult();
		}
	}
}
