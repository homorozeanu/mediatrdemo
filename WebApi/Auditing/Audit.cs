using System;
using System.Threading.Tasks;
using WebApi.Requests;

namespace WebApi.Auditing
{
	public class Audit : IAudit
	{
		public async Task NewUserCreatedRequest(CreateCustomerRequest createCustomerRequest)
		{
			Console.WriteLine($"Create New User Request for {createCustomerRequest}");
			await Task.Delay(TimeSpan.FromMilliseconds(20));
		}
	}
}
