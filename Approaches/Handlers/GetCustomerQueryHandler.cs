using Approaches.Messages;
using Approaches.Responses;
using MediatR;
using System;
using System.Threading;

namespace Approaches.Handlers
{
	public class GetCustomerQueryHandler : RequestHandler<GetCustomerQuery, CustomerResponse>
	{
		protected override CustomerResponse Handle(GetCustomerQuery request)
		{
			Thread.Sleep(500);
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Searching for customer '{request.CustomerId}'");

			var customerResponse = new CustomerResponse
			{
				Id = request.CustomerId,
				FullName = "Friendly customer"
			};

			return customerResponse;
		}
	}
}
