using System;
using System.Threading;
using System.Threading.Tasks;
using Approaches.Queries;
using MediatR;

namespace Approaches.Handlers
{
	public class GetCustomerNotificationLogHandler : INotificationHandler<GetCustomerQuery>
	{
		public async Task Handle(GetCustomerQuery notification, CancellationToken cancellationToken)
		{
			await Task.Delay(TimeSpan.FromMilliseconds(1000), cancellationToken);
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] [Log Handler] Logging search for customer request '{notification.CustomerId}'");
		}
	}
}
