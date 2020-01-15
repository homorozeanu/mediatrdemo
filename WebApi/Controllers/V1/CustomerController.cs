using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Auditing;
using WebApi.Entities;
using WebApi.Repositories;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Controllers.V1
{
	[ApiController]
	[Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
	    private readonly IMapper mapper;
	    private readonly IAudit audit;
	    private readonly ICustomerRepository customerRepository;

	    public CustomerController(
		    IMapper mapper,
			IAudit audit,
		    ICustomerRepository customerRepository)
	    {
		    this.mapper = mapper;
		    this.audit = audit;
		    this.customerRepository = customerRepository;
	    }

	    [HttpGet("")]
	    public async Task<IActionResult> GetAllCustomers()
	    {
		    var allCustomers = await customerRepository.GetAllCustomersAsync();
		    var customerResponses = mapper.Map<IList<CustomerResponse>>(allCustomers);
		    return Ok(customerResponses);
	    }

	    [HttpGet("{customerId}")]
	    public async Task<IActionResult> GetCustomer(Guid customerId)
	    {
		    var customer = await customerRepository.GetCustomerByIdAsync(customerId);
		    var customerResponse = mapper.Map<CustomerResponse>(customer);
		    return customerResponse == null ? (IActionResult) this.NotFound() : this.Ok(customerResponse);
	    } 

	    [HttpPost("")]
	    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
	    {
		    await audit.NewUserCreatedRequest(request);
		    var customer = mapper.Map<Customer>(request);
		    var addedCustomer = await customerRepository.CreateCustomerAsync(customer);
		    var customerResponse = mapper.Map<CustomerResponse>(addedCustomer);
		    return CreatedAtAction("GetCustomer", new {customerId = customer.Id}, customerResponse);
	    }
    }
}
