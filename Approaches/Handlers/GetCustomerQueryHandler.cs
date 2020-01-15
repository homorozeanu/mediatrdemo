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
		public async Task<CustomerResponse> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
		{
			await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] [Req/Res Handler] Searching for customer '{request.CustomerId}'");

			if (request.CustomerId == Guid.Empty)
			{
				throw new InvalidOperationException("[Req/Res Handler] Searching with an empty customer id is not allowed!");
			}

			var customerResponse = new CustomerResponse
			{
				Id = request.CustomerId,
				FullName = "Friendly customer"
			};

			return await Task.FromResult(customerResponse);
		}
	}
}
