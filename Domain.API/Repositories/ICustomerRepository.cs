using System;
using System.Threading.Tasks;

namespace Domain.API.Repositories
{
	public interface ICustomerRepository
	{
		Task<Customer> GetCustomerById(Guid id);
	}
}
