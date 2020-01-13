using Domain.API;
using Domain.API.Queries;
using Domain.API.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Handlers
{
	public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
	{
		private readonly ICustomerRepository customerRepository;

		public GetCustomerQueryHandler(ICustomerRepository customerRepository)
		{
			this.customerRepository = customerRepository;
		}

		public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
		{
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Searching for customer '{request.CustomerId}'");
			return await customerRepository.GetCustomerById(request.CustomerId);
		}
	}
}
