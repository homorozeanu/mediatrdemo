using System.Collections.Generic;
using MediatR;
using WebApi.Responses;

namespace WebApi.Queries
{
    public class GetAllCustomersQuery : IRequest<IList<CustomerResponse>>
    {
    }
}
