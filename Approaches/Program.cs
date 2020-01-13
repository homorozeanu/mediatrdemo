using MediatR;
using StructureMap;
using System;
using System.Threading.Tasks;
using System.Threading;
using Domain.Handlers;
using Domain.API.Queries;
using Domain.API.Repositories;
using DAL.Repositories;

namespace Approaches
{
	class Program
	{
		static async Task MainAsync()
		{
			var mediator = GetMediator();

			var query = new GetCustomerQuery(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"));
			var customer = await mediator.Send(query);

			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Found customer '{customer.FullName}'");
			Console.ReadLine();
		}

		private static IMediator GetMediator()
		{
			var container = new Container(cfg =>
			{
				cfg.For<ICustomerRepository>().Use<CustomerRepository>();

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
			MainAsync().GetAwaiter().GetResult();
		}
	}
}
