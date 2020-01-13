using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.API;
using Domain.API.Repositories;

namespace DAL.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private static IList<Customer> customers = new List<Customer>()
		{
			new Customer() {Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), FullName = "Freindly customer"},
			new Customer() {Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), FullName = " Not so friendly customer"}
		};

		public async Task<Customer> GetCustomerById(Guid id)
		{
			return await Task.FromResult(customers.SingleOrDefault(c => c.Id == id));
		}
	}
}
