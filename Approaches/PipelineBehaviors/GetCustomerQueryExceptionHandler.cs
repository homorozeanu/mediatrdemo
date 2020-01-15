using System;
using System.Threading;
using System.Threading.Tasks;
using Approaches.Queries;
using Approaches.Responses;
using MediatR.Pipeline;

namespace Approaches.PipelineBehaviors
{
	public class GetCustomerQueryExceptionHandler : AsyncRequestExceptionHandler<GetCustomerQuery, CustomerResponse>
	{
		protected override async Task Handle(
			GetCustomerQuery request, 
			Exception exception, 
			RequestExceptionHandlerState<CustomerResponse> state,
			CancellationToken cancellationToken)
		{
			await Task.Delay(TimeSpan.FromMilliseconds(200), cancellationToken);
			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] [Exception Handler] Handling exception type '{exception.GetType().Name}'");
			state.SetHandled();
		}
	}
}
