using Approaches.Messages;
using Approaches.Responses;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Approaches.Handlers
{
	public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerResponse>
	{
		public async Task<CustomerResponse> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
		{
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Searching for customer '{request.CustomerId}'");
			//await Task.Delay(TimeSpan.FromSeconds(2));

			var customerResponse = new CustomerResponse
			{
				Id = request.CustomerId,
				FullName = "Friendly customer"
			};

			return await Task.FromResult(customerResponse);
		}
	}
}
