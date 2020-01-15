using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Commands;
using WebApi.Queries;
using WebApi.Requests;

namespace WebApi.Controllers.V2
{
	[ApiController]
	[Route("api/v2/[controller]")]
    public class CustomerController : ControllerBase
    {
	    private readonly IMediator mediator;

	    public CustomerController(IMediator mediator)
	    {
		    this.mediator = mediator;
	    }

	    [HttpGet("")]
	    public async Task<IActionResult> GetAllCustomers()
	    {
		    var query = new GetAllCustomersQuery();
		    var customersResponse = await this.mediator.Send(query);
		    return this.Ok(customersResponse);
	    }

	    [HttpGet("{customerId}")]
	    public async Task<IActionResult> GetCustomer(Guid customerId)
	    {
		    var query = new GetCustomerByIdQuery(customerId);
		    var customerResponse = await this.mediator.Send(query);
		    return customerResponse == null ? (IActionResult) this.NotFound() : this.Ok(customerResponse);
	    } 

	    [HttpPost("")]
	    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
	    {
		    var command = new CreateCustomerCommand(request);
		    var customerResponse = await this.mediator.Send(command);
		    return CreatedAtAction("GetCustomer", new {customerId = customerResponse.Id}, customerResponse);
	    }
    }
}
