using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApi.Queries;
using WebApi.Repositories;
using WebApi.Responses;

namespace WebApi.Handlers
{
	public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IList<CustomerResponse>>
	{
		private readonly ICustomerRepository customerRepository;
		private readonly IMapper mapper;

		public GetAllCustomersQueryHandler(
			ICustomerRepository customerRepository,
			IMapper mapper)
		{
			this.customerRepository = customerRepository;
			this.mapper = mapper;
		}

		public async Task<IList<CustomerResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
		{
			var allCustomers = await customerRepository.GetAllCustomersAsync();
			var customerResponses = mapper.Map<IList<CustomerResponse>>(allCustomers);
			return await Task.FromResult(customerResponses);
		}
	}
}
