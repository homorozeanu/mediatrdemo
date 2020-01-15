using System.Threading.Tasks;
using WebApi.Requests;

namespace WebApi.Auditing
{
	public interface IAudit
	{
		Task NewUserCreatedRequest(CreateCustomerRequest createCustomerRequest);
	}
}
