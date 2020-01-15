using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApi.Auditing;
using WebApi.Commands;
using WebApi.Entities;
using WebApi.Repositories;
using WebApi.Responses;

namespace WebApi.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
	    private readonly ICustomerRepository customerRepository;
	    private readonly IAudit audit;
	    private readonly IMapper mapper;

	    public CreateCustomerCommandHandler(
		    ICustomerRepository customerRepository,
		    IAudit audit,
		    IMapper mapper)
	    {
		    this.customerRepository = customerRepository;
		    this.audit = audit;
		    this.mapper = mapper;
	    }

	    public async Task<CustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
	    {
		    await audit.NewUserCreatedRequest(request.Customer);
		    var customer = mapper.Map<Customer>(request.Customer);
		    var addedCustomer = await customerRepository.CreateCustomerAsync(customer);
		    var customerResponse = mapper.Map<CustomerResponse>(addedCustomer);
		    return await Task.FromResult(customerResponse);
	    }
    }
}
