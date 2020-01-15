using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
	    private readonly ConcurrentBag<Customer> customers = new ConcurrentBag<Customer>()
	    {
		    new Customer {Id = Guid.Parse("9D0D4187-FEE5-49D3-BDE6-FA0C6FB8B830"), Name = "Customer A"},
		    new Customer {Id = Guid.Parse("E5920B28-9554-41DE-A8A6-6764037A1DCC"), Name = "Customer B"},
		    new Customer {Id = Guid.Parse("6DF05BC7-1BE5-4954-B6FA-7FEB8D263811"), Name = "Customer C"}
	    };
        
	    public Task<List<Customer>> GetAllCustomersAsync()
	    {
		    return Task.FromResult(this.customers.ToList());
	    }

	    public Task<Customer> GetCustomerByIdAsync(Guid customerId)
	    {
		    return Task.FromResult(this.customers.SingleOrDefault(c => c.Id == customerId));
	    }

	    public Task<Customer> CreateCustomerAsync(Customer customer)
	    {
		    customer.Id = Guid.NewGuid();
		    customers.Add(customer);
		    return Task.FromResult(customer);
	    }
    }
}
