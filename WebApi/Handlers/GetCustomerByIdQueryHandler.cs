using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApi.Queries;
using WebApi.Repositories;
using WebApi.Responses;

namespace WebApi.Handlers
{
	public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
	{
		private readonly ICustomerRepository customerRepository;
		private readonly IMapper mapper;

		public GetCustomerByIdQueryHandler(
			ICustomerRepository customerRepository,
			IMapper mapper)
		{
			this.customerRepository = customerRepository;
			this.mapper = mapper;
		}


		public async Task<CustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
		{
			var customer = await customerRepository.GetCustomerByIdAsync(request.CustomerId);
			var customerResponse = mapper.Map<CustomerResponse>(customer);
			return await Task.FromResult(customerResponse);
		}
	}
}
