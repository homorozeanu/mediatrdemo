using Approaches.Responses;
using MediatR;
using System;

namespace Approaches.Messages
{
	public class GetCustomerQuery : IRequest<CustomerResponse>
    {
		public GetCustomerQuery(Guid customerId)
		{
			CustomerId = customerId;
		}

		public Guid CustomerId { get; }
	}
}
