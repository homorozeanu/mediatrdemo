using System;
using MediatR;
using WebApi.Responses;

namespace WebApi.Queries
{
	public class GetCustomerByIdQuery : IRequest<CustomerResponse>
	{
		public Guid CustomerId { get; }

		public GetCustomerByIdQuery(Guid customerId)
		{
			CustomerId = customerId;
		}
	}
}
