using System;
using Approaches.Responses;
using MediatR;

namespace Approaches.Queries
{
	public class GetCustomerQuery : IRequest<CustomerResponse>, INotification
    {
		public GetCustomerQuery(Guid customerId)
		{
			CustomerId = customerId;
		}

		public Guid CustomerId { get; }
	}
}
