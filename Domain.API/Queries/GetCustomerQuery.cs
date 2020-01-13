using MediatR;
using System;

namespace Domain.API.Queries
{
	public class GetCustomerQuery : IRequest<Customer>
    {
		public GetCustomerQuery(Guid customerId)
		{
			CustomerId = customerId;
		}

		public Guid CustomerId { get; }
	}
}
