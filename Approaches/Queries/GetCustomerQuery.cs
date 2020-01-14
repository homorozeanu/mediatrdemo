using System;
using MediatR;

namespace Approaches.Queries
{
	public class GetCustomerQuery : IRequest
    {
		public GetCustomerQuery(Guid customerId)
		{
			CustomerId = customerId;
		}

		public Guid CustomerId { get; }
	}
}
