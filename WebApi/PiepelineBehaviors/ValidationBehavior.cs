using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace WebApi.PiepelineBehaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> validators;

		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			this.validators = validators;
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			// Pre
			var context = new ValidationContext(request);
			var failures = validators
				.Select(x => x.Validate(context))
				.SelectMany(x => x.Errors)
				.Where(x => x != null)
				.ToList();

			if (failures.Any())
			{
				throw new ValidationException(failures); // using Result<TSucess, TFailure> would be better was
			}

			return next();

			// Post -> nothing here at the moment
		}
	}
}
