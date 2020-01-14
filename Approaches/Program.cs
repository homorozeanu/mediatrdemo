using Approaches.Handlers;
using MediatR;
using StructureMap;
using System;
using System.Threading.Tasks;
using System.Threading;
using Approaches.Queries;

namespace Approaches
{
	class Program
	{
		static async Task MainAsync()
		{
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] On MainAsync");

			var mediator = GetMediator();

			var query = new GetCustomerQuery(Guid.NewGuid());
			var customer = await mediator.Send(query);

			// The default implementation of Publish loops through
			// the notification handlers and awaits each one.
			// This ensures each handler is run after one another.
			await mediator.Publish(query);

			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Found customer '{customer.FullName}'");
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
