using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Approaches.Queries;

namespace Approaches.Handlers
{
	public class GetCustomerQueryHandler : AsyncRequestHandler<GetCustomerQuery>
	{
		protected override async Task Handle(GetCustomerQuery request, CancellationToken cancellationToken)
		{
			await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Searching for customer '{request.CustomerId}' (fire and forget)");
		}
	}
}
