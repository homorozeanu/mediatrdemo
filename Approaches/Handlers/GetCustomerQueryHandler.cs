using Approaches.Responses;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Approaches.Queries;

namespace Approaches.Handlers
{
	public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerResponse>
	{
		private int counter = 0;

		public async Task<CustomerResponse> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
		{
			counter++;
			await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Searching for customer '{request.CustomerId}' [{counter} time(s)]");

			var customerResponse = new CustomerResponse
			{
				Id = request.CustomerId,
				FullName = "Friendly customer"
			};

			return await Task.FromResult(customerResponse);
		}
	}
}
