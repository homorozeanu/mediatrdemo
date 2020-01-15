using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Requests;

namespace WebApi.Repositories
{
    public interface ICustomerRepository
    {
	    Task<List<Customer>> GetAllCustomersAsync();
	    Task<Customer> GetCustomerByIdAsync(Guid customerId);
	    Task<Customer> CreateCustomerAsync(Customer customer);
    }
}
