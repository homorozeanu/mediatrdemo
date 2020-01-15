using MediatR;
using WebApi.Entities;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Commands
{
	public class CreateCustomerCommand : IRequest<CustomerResponse>
	{
		public CreateCustomerRequest Customer { get; }

		public CreateCustomerCommand(CreateCustomerRequest customer)
		{
			Customer = customer;
		}
	}
}
