using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Approaches.Queries;
using MediatR;

namespace Approaches.Handlers
{
	public class GetCustomerNotificationAuditHandler : INotificationHandler<GetCustomerQuery>
	{
		public async Task Handle(GetCustomerQuery notification, CancellationToken cancellationToken)
		{
			await Task.Delay(TimeSpan.FromMilliseconds(1000), cancellationToken);
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Audit search for customer request '{notification.CustomerId}'");
			if (notification.CustomerId == Guid.Empty)
			{
				throw new DataException("Audit database not found!");
			}
		}
	}
}
